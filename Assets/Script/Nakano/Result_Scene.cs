using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result_Scene : MonoBehaviour
{
    [SerializeField]
    string nextScene = "NewScene";

    [SerializeField] Text Move_T;

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
        Move_T.text = (10 - timer).ToString("f0");

        if (timer >= 9)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
