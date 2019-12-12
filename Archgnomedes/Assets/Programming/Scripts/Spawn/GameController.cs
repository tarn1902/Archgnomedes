/*----------------------------------------
File Name: GameController.cs
Purpose: Controls overview of game
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archgnomedes
{
    
    public class GameController : MonoBehaviour
    {
        [Header("Attributes")]
        private GameObject player = null;
        private PlayerLife life = null;
        private GameObject fadeDeath = null;
        private FadeDeath fadeDeathScript = null;
        public int currentSpawnLocation = 0;

        //-----------------------------------------------------------
        // Checks if gameobject already exists, get components
        //-----------------------------------------------------------
        private void Start()
        {
            if (GameObject.FindGameObjectsWithTag("GameController").Length == 1)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            if (fadeDeath == null)
            {
                fadeDeath = GameObject.FindGameObjectWithTag("DeathFade");
                if (fadeDeath != null)
                {
                    fadeDeathScript = fadeDeath.GetComponent<FadeDeath>();
                }
            }
        }

        //-----------------------------------------------------------
        // If player dies, ends game
        //-----------------------------------------------------------
        void Update()
        {
            if (SceneManager.GetSceneByBuildIndex(0) != SceneManager.GetActiveScene())
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        life = player.GetComponent<PlayerLife>();
                    }
                }
                else if (life.isDead)
                {
                    if (fadeDeathScript.startFade == false)
                    {
                        fadeDeathScript.startFade = true;
                    }
                    if (fadeDeathScript.fadeFinish)
                    {
                        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
        }
    }
}
