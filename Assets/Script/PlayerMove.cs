using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerParamClass paramClass = PlayerParamClass.GetInstance();

    //加速減速
    [SerializeField, Header("加速値")]
    private float accele;
    [SerializeField, Header("減速値"), Tooltip("0:通常 1:上り坂 2:下り坂")]
    private float[] decele;
    private sbyte setDecele;　

    private void Start()
    {
        SetDecele(0);
    }
    void Update()
    {
        //加速入力
        if (Input.GetKeyDown(KeyCode.UpArrow))
            paramClass.SpeedFluctuation(accele);
        //減速(毎秒)

        paramClass.SpeedFluctuation(decele[setDecele]);

        ActionPlayerMove();
    }
    private void ActionPlayerMove()
    {
        Vector3 movePos = transform.position;
        movePos.z += paramClass.playerSpeed;
        transform.position = movePos;
    }
    //選んだ減速値をセットする
    private void SetDecele(sbyte num)
    {
        setDecele = num;
    }
}
