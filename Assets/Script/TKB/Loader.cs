using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    bool onecFlag;

    AsyncOperation async;

    [SerializeField] Slider slider;
    [SerializeField] string NextScene;

    // Start is called before the first frame update
    void Start()
    {
        onecFlag = false;
        slider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onecFlag == false)
        {
            onecFlag = true;
            slider.enabled = true;
            StartCoroutine("LoadData");
        }

        if (slider.value == 1f)
        {
            //if (Input.GetMouseButton(0))
                async.allowSceneActivation = true;
        }
    }

    IEnumerator LoadData()
    {
        async = SceneManager.LoadSceneAsync(NextScene);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;

            yield return null;
        }
    }
}