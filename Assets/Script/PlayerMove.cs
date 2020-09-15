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
    [SerializeField, Header("減速値"), Tooltip("0:通常 1:上り坂 2:下り坂")]
    private float[]
        decele;
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

    }

    //プレイヤーを動かす
    private void ActionPlayerMove()
    {
        //前方移動
        Vector3 movePos = Vector3.zero;
        movePos.z += paramClass.playerSpeed;
        playerRB.velocity = movePos;

        //左右移動
        
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
}
