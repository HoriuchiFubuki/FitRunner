using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSystem : MonoBehaviour
{

    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    [SerializeField]
    string nextScene = "NewScene";

    public GameObject ClearLogo;
    public GameObject Players;
    public GameObject Time;
    public GameObject BGM;

    public AudioClip Goal_se;

    AudioSource SoundEffecter;


    void Start()
    {
        SoundEffecter = gameObject.AddComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            BGM.gameObject.SetActive(false);

            SoundEffecter.PlayOneShot(Goal_se);

            SoundEffecter.mute = mute;
            SoundEffecter.volume = vol;

            //クリアしたよ表示
            ClearLogo.gameObject.SetActive(true);

            //しばらくしたらリザルトへ
            Invoke("ChangeScrene", 6f);

            //今回のタイムをセーブしとく
            PlayerPrefs.SetFloat("NewScore", StateUI.stageTime);
            PlayerPrefs.Save();

            Players.GetComponent<PlayerMove>().enabled = false;
            Time.gameObject.SetActive(false);
        }
    }

    void ChangeScrene()
    {
        SceneManager.LoadScene(nextScene);
    }

}
