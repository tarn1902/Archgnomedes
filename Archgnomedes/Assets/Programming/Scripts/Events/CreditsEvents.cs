/*----------------------------------------
File Name: CreditsEvents.cs
Purpose: Holds funtions for pause scene
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEvents : MonoBehaviour
{
    //-----------------------------------------------------------
    // Closes Credits
    //-----------------------------------------------------------
    public void CloseCredits()
    {
        SceneManager.UnloadSceneAsync("Credits");
    }
}
