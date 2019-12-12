/*----------------------------------------
File Name: FadeLife.cs
Purpose: remvoes cover from screen
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.UI;

public class FadeLife : MonoBehaviour
{
    public bool startFadeIn = true;
    public bool startFadeOut = false;
    public bool fadeFinish = false;
    Image image = null;
    float change = 0;

    //-----------------------------------------------------------
    // Gets components
    //-----------------------------------------------------------
    void Start()
    {
        image = GetComponent<Image>();
    }

    //-----------------------------------------------------------
    // Will start fade out image
    //-----------------------------------------------------------
    void Update()
    {
        if (startFadeIn)
        {
            if (change < 1.1f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, change));
                change += 0.1f;
            }
            else
            {
                startFadeIn = false;
                change = 0;
            }

        }
        if (startFadeOut)
        {
            if (change < 1.1f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, change));
                change += 0.1f;
            }
            else
            {
                startFadeOut = false;
                fadeFinish = true;
            }

        }
    }
}
