/*----------------------------------------
File Name: FadeDeath.cs
Purpose: covers screen
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.UI;

public class FadeDeath : MonoBehaviour
{
    public bool startFade = false;
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
    // if turned on, will start fade
    //-----------------------------------------------------------
    void Update()
    {
        if (startFade)
        {
            if (change < 1.1f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, change));
                change += 0.1f;
            }
            else
            {
                fadeFinish = true;
            }
            
        }
    }
}
