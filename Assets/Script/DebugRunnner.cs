using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRunnner : MonoBehaviour
{

    PlayerParamClass
    paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    Vector3 ContinuePos;
    public bool AutoRun;
    [Range(0.0f, 40.0f)]
    public float
        AutoRunSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AutoRun && paramClass.playerSpeed < AutoRunSpeed)
            paramClass.isRun = true;
        if (Input.GetKey(KeyCode.C))
            Continue();
    }

    private void Continue()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        pos.x = ContinuePos.x;
        pos.y = ContinuePos.y;
        pos.z = ContinuePos.z;
        myTransform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContinuePoint"))
            Continue();
    }
}
