using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result_Scene : MonoBehaviour
{
    [SerializeField]
    string nextScene = "NewScene";

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
