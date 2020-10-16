using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField]
    GameObject dmgEfPrefub;
    GameObject dmgEf;
    [SerializeField]
    Transform parentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Instantiate(dmgEfPrefub, dmgEfPrefub.transform.position, 
                                Quaternion.identity, parentCanvas);
        }
    }
}
