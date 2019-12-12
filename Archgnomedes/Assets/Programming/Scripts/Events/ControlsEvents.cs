/*----------------------------------------
File Name: ControlEvents.cs
Purpose: Holds funtions for Control scene
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsEvents : MonoBehaviour
{
    //-----------------------------------------------------------
    // Close Control scene
    //-----------------------------------------------------------
    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync("Controls");
    }
}
