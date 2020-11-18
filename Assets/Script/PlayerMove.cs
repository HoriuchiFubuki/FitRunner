using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    //加速減速
    [SerializeField, Header("加速値")]
    private float
        accele;
    [SerializeField, Header("最高速度値")]
    private float
    fullSpeed;
    [SerializeField, Header("減速値"), Tooltip("0:通常 1:上り坂 2:下り坂")]
    private float[]
        decele;
    [SerializeField, Header("ジャンプ力")]
    private float
    jumpForce;

    private bool
    isGround;
    private sbyte
        setDecele;
    private Rigidbody
        playerRB;
    private CapsuleCollider
        playerCol;
    private Vector3
        pColCenter;
    private float
         pColHeight;
    private IEnumerator
        regularlyUpdate;       

    private enum MyFunction
    {
        DECELERATE,
        MAXVAL
    }

    private void Start()
    {
        isGround = true;
        playerRB = GetComponent<Rigidbody>();
        playerCol = GetComponent<CapsuleCollider>();
        pColCenter = playerCol.center;
        pColHeight = playerCol.height;

        SetDecele(0);

        //減速処理のコルーチン呼び出し
        regularlyUpdate = RegularlyUpdate(MyFunction.DECELERATE);
        StartCoroutine(regularlyUpdate);
    }
    private void FixedUpdate()
    {
        //        ActionPlayerMove1();
                  ActionPlayerMove2();
    }
    void Update()
    {
        //加速入力
        if ((paramClass.isRun || Input.GetKeyDown(KeyCode.UpArrow))&& paramClass.playerSpeed < fullSpeed - accele)
        {
            paramClass.SpeedFluctuation(accele);
            paramClass.isRun = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            paramClass.SpeedFluctuation_LR(accele_LR / 60f);
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            paramClass.SpeedFluctuation_LR(-accele_LR / 60f);
        else paramClass.SpeedFluctuation_LR(0);
        if (Input.GetKeyDown(KeyCode.Space) || isGround && jumpForce <= paramClass.playerPos.y * 10)
            paramClass.SpeedFluctuation_Jump(jumpForce);
        else if (!isGround)
            paramClass.SpeedFluctuation_Jump(0);

        if (Input.GetKey(KeyCode.LeftControl))
        {            
            playerCol.height = pColHeight / 2f;
            playerCol.center = new Vector3(0, pColCenter.y-(pColHeight/2 - playerCol.height/2), 0);
        }
        else if (playerCol.height != pColHeight)
        {
            playerCol.height = pColHeight;
            playerCol.center = pColCenter;
        }
    }

    //プレイヤーを動かす
    private void ActionPlayerMove1()
    {
        //前後移動
        Vector3 moveXY = transform.position;

        //左右移動
        moveXY.x = paramClass.playerPos.x * 10;

        //縦方向移動(ジャンプ)
        moveXY.y = paramClass.playerPos.y * 20;

       // transform.position = moveXY;

        Vector3 moveZ = playerRB.velocity;
        //前方移動
        moveZ.z = paramClass.playerSpeed;
        playerRB.velocity = moveZ;
    }

    private void ActionPlayerMove2()//現在使用しているのはこちら
    {
        Vector3 movePos = Vector3.zero;

        //前方移動
        movePos.z = paramClass.playerSpeed;

        //左右移動
        movePos.x = paramClass.playerPos.x * 20;

        //縦方向移動(ジャンプ)
        //if (isGround && jumpForce <= paramClass.playerPos.y * 20)
        movePos.y = paramClass.playerJumpforce;
        //movePos.y = paramClass.playerPos.y * 20; //(プレイヤー座標の直接反映)
        if (!isGround)
            movePos.y = playerRB.velocity.y; //空中時にY要素のみ変化なし（自由落下）
        playerRB.velocity = movePos;
    }

    /// <summary>
    /// 定期的に更新する処理用のコルーチン
    /// *2回目以降はUpdateの処理後に実行するので注意*
    /// </summary>
    /// <param name="val">実行する処理</param>
    /// <returns>何秒ごとに実行するか(1.0f = 1秒)</returns>
    IEnumerator RegularlyUpdate(MyFunction val)
    {
        switch (val)
        {
            case MyFunction.DECELERATE:
                for (;;)
                {
                    Decelerate();
                    yield return new WaitForSeconds(.1f);
                }
            default:
                break;
        }
    }

    //選んだ減速値をセットする
    private void SetDecele(sbyte num)
    {
        setDecele = num;
    }
    //減速させる
    private void Decelerate()
    {
        paramClass.SpeedFluctuation(decele[setDecele]);
    }
    //接地判定
    private void OnCollisionStay(Collision collision)
    {
        isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
}
