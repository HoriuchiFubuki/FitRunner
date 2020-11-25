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
    private bool isGround;
    private bool isJump;
    [SerializeField, Header("アニメーション変更速度"), Tooltip("0:idle 1:walk 2:jog 3:dash 4:boost")]
    private float[]
        shift_CharaAnime;
    [SerializeField, Header("ジャンプ判定高度")]
    private float
        hight_CharaAnime;

    // Start is called before the first frame update
    void Start()
    {
        this.Anm = GetComponent<Animator>();
        this.Run = 0;
        this.Jump_S = 0;
        isGround = true;
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        ///速度に応じて移動アニメーションを変化
        switch (paramClass.playerSpeed)
        {
            case float n when n == 0:
                Run = 0;
                break;
            case float n when 0 <= n && n < shift_CharaAnime[0]:
                Run = 1;
                break;
            case float n when shift_CharaAnime[0] <= n && n < shift_CharaAnime[1]:
                Run = 2;
                break;
            case float n when shift_CharaAnime[1] <= n && n < shift_CharaAnime[2]:
                Run = 3;
                break;
            default:
                Run = 4;
                break;
        }

        ///プレイヤーの高さが値を超えるとジャンプアニメーションを再生
        if ((hight_CharaAnime <= paramClass.playerPos.y * 20 || (Input.GetKeyDown(KeyCode.Space) || paramClass.isJump)) && isGround)
        //if(paramClass.rightKneeUpNow && paramClass.leftKneeUpNow)
        {
            Jump_S = Random.Range(0, 3);
            Anm.SetInteger("Jump_Select", Jump_S);
            Anm.SetBool("Runs", false);
            Anm.SetBool("Jump", true);
            isJump = true;

        }
        else if (isGround || isJump)
        {
            Anm.SetBool("Runs", true);
            Anm.SetBool("Jump", false);
            isJump = false;
        }

        ///プレイヤーがしゃがむとスライディング/ローリングアニメーションを再生
        if (Input.GetKey(KeyCode.LeftControl) && isGround && paramClass.playerSpeed != 0)
        {
            Anm.SetBool("Sliding",true);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && isGround && paramClass.playerSpeed == 0)
        {
            Anm.SetBool("Roll",true);
        }
        else
        {
            Anm.SetBool("Sliding", false);
            Anm.SetBool("Roll", false);
        }
        Anm.SetInteger("RunSpeed", Run);


        ///プレイヤーが空中滞在時は落下アニメーションを再生
        if (!isGround)
        {
            Anm.SetBool("fall", true);
        }
        else
        {
            Anm.SetBool("fall", false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.collider.CompareTag("Obstacle"))
            isGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag("Obstacle"))
            isGround = false;
    }

}