﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank_Bar : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    //private
    private Image image;
    private float time;


    void Start()
    {
        //アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<Image>())
        {
            image = this.gameObject.GetComponent<Image>();
        }
    }

    void Update()
    {
        //オブジェクトのAlpha値を更新
            image.color = GetAlphaColor(image.color);
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
