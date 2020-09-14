using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのパラメーター管理クラス
/// </summary>
public class PlayerParamClass
{
    private static PlayerParamClass playerParamClass;

    /// <summary>
    /// コンストラクタ（初期化）
    /// </summary>
    public PlayerParamClass()
    {
        playerLife = 3;
        playerSpeed = 0;
    }

    /// <summary>
    /// PlayerParamClassをシングルトンパターンでインスタンス化
    /// </summary>
    /// <returns></returns>
    public static PlayerParamClass GetInstance()
    {
        //singleton
        if (playerParamClass == null)
        {
            playerParamClass = new PlayerParamClass();
        }
        return playerParamClass;
    }

    public sbyte
        playerLife { get; private set; }
    const sbyte MaxLife = 3;
    public float
        playerSpeed{ get; private set; }

    /// <summary>
    /// プレイヤーのライフ変動を格納
    /// </summary>
    /// <param name="val">変動する値</param>
    public void LifeFluctuation(sbyte val)
    {
        playerLife += val;
        if (playerLife > MaxLife)
            playerLife = MaxLife;
        //ライフがなくなったらGameOverへ遷移
        if (playerLife <= 0)
            playerDie();
    }
    private void playerDie()
    {
        Debug.Log("GameOver");
    }

    /// <summary>
    /// プレイヤーの速度変動を格納
    /// </summary>
    /// <param name="val">変動する値</param>
    public void SpeedFluctuation(float val)
    {
        playerSpeed += val;
        if (playerSpeed < 0)
            playerSpeed = 0;
    }
}