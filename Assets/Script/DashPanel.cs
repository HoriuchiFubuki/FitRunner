using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPanel : MonoBehaviour
{
    PlayerParamClass
     paramClass = PlayerParamClass.GetInstance();

    [Range(0.0f, 25.0f)]
    public float
        PanelSpeed = 1.0f;

    private bool OnPanel = false;

    // Update is called once per frame
    void Update()
    {
        if (OnPanel)
            paramClass.SpeedFluctuation(PanelSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DashPanel")
        {
            OnPanel = true;
            Debug.Log("aaaa");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DashPanel")
        {
            OnPanel = false;
        }
    }

}
