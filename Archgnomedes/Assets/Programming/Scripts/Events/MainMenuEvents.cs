/*----------------------------------------
File Name: MainMeuEvents.cs
Purpose: Holds funtions for main menu 
    scene
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Archgnomedes
{
    public class MainMenuEvents : MonoBehaviour
    {
        public Textcrawl textcrawl = null;
        //-----------------------------------------------------------
        // Opens game scene
        //-----------------------------------------------------------
        public void OpenGame()
        {
            textcrawl.activate = true;
            gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        //-----------------------------------------------------------
        // Opens settings scene
        //-----------------------------------------------------------
        public void OpenSettings()
        {
            SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        }

        //-----------------------------------------------------------
        // Opens credits scene
        //-----------------------------------------------------------
        public void OpenCredits()
        {
            SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
        }

        //-----------------------------------------------------------
        // closes application
        //-----------------------------------------------------------
        public void Quit()
        {
            Application.Quit();
        }
    }
}
