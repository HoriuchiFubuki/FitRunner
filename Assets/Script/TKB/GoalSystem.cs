using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSystem : MonoBehaviour
{

    PlayerParamClass
        paramClass = PlayerParamClass.GetInstance();

    [SerializeField]
    string nextScene = "NewScene";

    public GameObject ClearLogo;
    public GameObject Players;
    public GameObject Time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {  

            //クリアしたよ表示
            ClearLogo.gameObject.SetActive(true);

            //しばらくしたらリザルトへ
            Invoke("ChangeScrene", 3f);

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
