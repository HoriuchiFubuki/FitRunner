using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text YouScoreText;
    [SerializeField] Text FirstTime;

    // Start is called before the first frame update
    void Start()
    {
        float YourScore = PlayerPrefs.GetFloat("NewScore");
        YouScoreText.text = YourScore.ToString("f2");
        float F_Score = PlayerPrefs.GetFloat("FirstScore");
        switch(YourScore)
        {
            case float n when n <= F_Score:
            FirstTime.text = YourScore.ToString("f2");
            PlayerPrefs.SetFloat("FirstScore", YourScore);
                break;

            case float n when n >= F_Score:
            FirstTime.text = "1st  " + F_Score.ToString("f2");
                break;
        }
        

        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
