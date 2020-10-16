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
    [SerializeField, Header("左右加速値")]
    private float
        accele_LR;
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
        SetDecele(0);

        //減速処理のコルーチン呼び出し
        regularlyUpdate = RegularlyUpdate(MyFunction.DECELERATE);
        StartCoroutine(regularlyUpdate);
    }
    private void FixedUpdate()
    {
        ActionPlayerMove();
    }
    void Update()
    {
        //加速入力
        if (Input.GetKeyDown(KeyCode.UpArrow))
            paramClass.SpeedFluctuation(accele);

        //左右方向入力
        if (Input.GetKey(KeyCode.RightArrow))
            paramClass.SpeedFluctuation_LR(accele_LR/ 60f);
        else if (Input.GetKey(KeyCode.LeftArrow))
            paramClass.SpeedFluctuation_LR(-accele_LR/ 60f);
        else paramClass.SpeedFluctuation_LR(0);

        if (Input.GetKeyDown(KeyCode.Space))
            paramClass.SpeedFluctuation_Jump(jumpForce);
        else if(!isGround)
            paramClass.SpeedFluctuation_Jump(0);
    }

    //プレイヤーを動かす
    private void ActionPlayerMove()
    {       
        Vector3 movePos = Vector3.zero;
        if (!isGround)
            movePos = playerRB.velocity;

        //前方移動
        movePos.z = paramClass.playerSpeed;

        //左右移動
        movePos.x = paramClass.playerSpeed_LR;

        //とりあえずジャンプ       
        movePos.y = paramClass.playerJumpforce;
        

        if (isGround)
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
        if(!collision.collider.CompareTag("Obstacle"))
            isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag("Obstacle"))
            isGround = false;
    }
}
