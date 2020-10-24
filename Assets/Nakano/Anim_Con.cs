using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Anim_Con : MonoBehaviour
{
    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    Animator Anm;
    private int Run;
    private int Jump_S;
    private bool Grn;

    // Start is called before the first frame update
    void Start()
    {
        this.Anm = GetComponent<Animator>();
        this.Run = 0;
        this.Jump_S = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (paramClass.playerSpeed >= 1.0f)
        {
            Run = 1;
        }

        if (paramClass.playerSpeed > 5.0f)
        {
            Run = 2;
        }

        if (paramClass.playerSpeed > 10.0f)
        {
            Run = 3;
        }

        if (paramClass.playerSpeed > 20.0f)
        {
            Run = 4;
        }

        if (!Grn)
        {
            Jump_S = Random.Range(0, 3);
            Anm.SetInteger("Jump_Select", Jump_S);
            Anm.SetBool("Runs", false);
            Anm.SetBool("Jump", true);
        }

        if (Grn)
        {
            Anm.SetBool("Runs", true);
            Anm.SetBool("Jump", false);
        }
        Anm.SetInteger("RunSpeed", Run);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.CompareTag("Obstacle"))
            Grn = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag("Obstacle"))
            Grn = false;
    }
}
