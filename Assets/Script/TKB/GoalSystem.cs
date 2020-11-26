using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSystem : MonoBehaviour
{
    [SerializeField]
    string nextScene = "NewScene";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("NewScore", StateUI.stageTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextScene);
        }
    }
}
