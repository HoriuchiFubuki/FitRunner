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
    public GameObject BGM;

    private float count;
    private bool[] seBool = new bool[4];

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    [SerializeField]
    AudioClip[] Count_se = new AudioClip[4];

    AudioSource SoundEffecter;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        for(int i = 0; i < 4; i++)
        {
            seBool[i] = true;
        }

        SoundEffecter = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        var img = GetComponent<Image>();

        switch (count)
        {
            case float n when n >= 0.03 && n < 1:
                Se_ring(0);
                break;

            case float n when n >= 1 && n < 2:
                img.sprite = image_2;
                Se_ring(1);
                break;

            case float n when n >= 2 && n < 3:
                img.sprite = image_1;
                Se_ring(2);
                break;

            case float n when n >= 3 && n < 4:
                img.sprite = image_start;
                Se_ring(3);
                break;

            case float n when n > 4:
                timer.gameObject.SetActive(true);
                Players.GetComponent <PlayerMove> ().enabled = true;
                BGM.GetComponent<BgmController>().enabled = true;
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void Se_ring(int j)
    {
        if (seBool[j])
        {
            SoundEffecter.PlayOneShot(Count_se[j]);

            SoundEffecter.mute = mute;
            SoundEffecter.volume = vol;
            seBool[j] = false;
        }
    }
}
