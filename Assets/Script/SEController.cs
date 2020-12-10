using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    Animator Anm;

    [Range(0.0f, 1.0f)]
    public float
        vol = 0.1f;
    public bool
        mute = false;

    [SerializeField]
    private float
        delay = 0.1f;

    public AudioClip se;

    AudioSource SoundEffecter;

    void Start()
    {
        SoundEffecter = gameObject.AddComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            SoundEffecter.PlayOneShot(se);

        SoundEffecter.mute = mute;
        SoundEffecter.volume = vol;
    }
}
