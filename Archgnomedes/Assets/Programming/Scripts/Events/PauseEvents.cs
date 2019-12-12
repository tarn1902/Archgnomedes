/*----------------------------------------
File Name: PauseEvents.cs
Purpose: Holds funtions for pause scene
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEvents : MonoBehaviour
{
    private GameObject pauseMenu = null;
    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }
    //-----------------------------------------------------------
    // Opens menu scene
    //-----------------------------------------------------------
    public void RunMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    //-----------------------------------------------------------
    // Closes pause scene
    //-----------------------------------------------------------
    public void ClosePauseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    //-----------------------------------------------------------
    // Open Control scene
    //-----------------------------------------------------------
    public void OpenControls()
    {
        SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
    }
}
