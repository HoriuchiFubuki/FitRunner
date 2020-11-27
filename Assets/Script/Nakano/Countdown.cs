using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public Sprite image_2;
    public Sprite image_1;
    public Sprite image_start;

    public GameObject timer;
    public GameObject Players;
    public GameObject Speed_M;

    private float count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        var img = GetComponent<Image>();
        switch (count)
        {
            case float n when n >= 1 && n < 2:
                img.sprite = image_2;
                break;
            case float n when n >= 2 && n < 3:
                img.sprite = image_1;
                break;
            case float n when n >= 3 && n < 4:
                img.sprite = image_start;
                break;
            case float n when n > 4:
                timer.gameObject.SetActive(true);
                Speed_M.gameObject.SetActive(true);
                Players.GetComponent <PlayerMove> ().enabled = true;
                this.gameObject.SetActive(false);
                break;
        }
    }
}
