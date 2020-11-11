using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class HitChecker : MonoBehaviour
{
    [SerializeField]
    GameObject dmgEfPrefub;
    [SerializeField]
    Transform parentCanvas;

    [SerializeField]
    GameObject childObj;

    Renderer renderer;
    private bool hitState;
    public float blinkInterval;
    public float allBlinkTime;

    // Start is called before the first frame update
    void Start()
    {
        hitState = false;
        renderer = childObj.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitState)
        {
            hitState = false;
            StartCoroutine("CharaBlinking");
        }
    }

    IEnumerator CharaBlinking()
    {
        float time = 0f;
        while (time > allBlinkTime)
        {           
            renderer.enabled = !renderer.enabled;
            time += Time.deltaTime;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            StartCoroutine(OnceHit(0.1f, collision));
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        hitState = true;
        if (other.CompareTag("Obstacle"))
        {
            Instantiate(dmgEfPrefub, dmgEfPrefub.transform.position,
                                Quaternion.identity, parentCanvas);
        }
    }

    private IEnumerator OnceHit(float waitTime, Collision collision)
    {
        yield return new WaitForSeconds(waitTime);
        collision.collider.isTrigger = true;
    }
}
