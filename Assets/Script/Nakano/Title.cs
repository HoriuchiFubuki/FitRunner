using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    PlayerParamClass
       paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    string nextScene = "NewScene";

    [SerializeField, Header("ジャンプ判定高度")]
    private float
        hight_CharaAnime;

    public GameObject Cam;
    public GameObject Logo;
    public GameObject Main;

    Animator Anime;
    private bool isJump;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        this.Anime = GetComponent<Animator>();
        Main.gameObject.SetActive(true);
        Logo.gameObject.SetActive(true);
        Cam.gameObject.SetActive(false);
        isJump = false;
        isGround = true;
    }

    // Update is called once per frame
    void Update()
    {
    if (hight_CharaAnime <= paramClass.playerPos.y * 20 || Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Anime.SetBool("Jump",true);
            isJump = true;
            Invoke("Ca", 1.5f);
        }
    else if (isGround || isJump)
        {
            Anime.SetBool("Jump", false);
            isJump = false;
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

    void Ca()
    {
        Main.gameObject.SetActive(false);
        Logo.gameObject.SetActive(false);
        Cam.gameObject.SetActive(true);
        Invoke("Scene", 5f);
    }

    void Scene()
    {
        SceneManager.LoadScene(nextScene);
    }

}
