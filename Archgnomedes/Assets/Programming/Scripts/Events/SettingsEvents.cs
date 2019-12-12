/*----------------------------------------
File Name: SettingsEvents.cs
Purpose: Holds funtions for Settings scene
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsEvents : MonoBehaviour
{
    [Header("Parts of Canvas")]
    public AudioMixer audioMixer;
    public Dropdown quality;
    public Slider volume;
    public Dropdown resolution;
    public Toggle fullScreenToggle;

    private int currentResolution;

    Resolution[] resolutions;

    //-----------------------------------------------------------
    // Sets up Resolution list and sets all pre set settings to 
    // menu
    //-----------------------------------------------------------
    private void Start()
    {
        quality.value = QualitySettings.GetQualityLevel();
        float currentVolume = 0;
        audioMixer.GetFloat("Volume", out currentVolume);
        volume.value = currentVolume;
        fullScreenToggle.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions;
        resolution.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolution.AddOptions(options);
        resolution.value = currentResolution;
        resolution.RefreshShownValue();
    }

    //-----------------------------------------------------------
    // Sets Master volume of game
    //-----------------------------------------------------------
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    //-----------------------------------------------------------
    // Sets Quality of game
    //-----------------------------------------------------------
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //-----------------------------------------------------------
    // Sets screen mode of game
    //-----------------------------------------------------------
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //-----------------------------------------------------------
    // Sets resolution of game
    //-----------------------------------------------------------
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    //-----------------------------------------------------------
    // Close settings scene
    //-----------------------------------------------------------
    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    //-----------------------------------------------------------
    // Open Control scene
    //-----------------------------------------------------------
    public void OpenControls()
    {
        SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
    }
}
