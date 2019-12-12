/*----------------------------------------
File Name: EnemySpawn.cs
Purpose: Spawns Enemies to set positions
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Archgnomedes
{
    public class EnemySpawn : MonoBehaviour
    {
        [Header("Spawn Settings")]
        public Transform[] spawnLocations;
        public GameObject enemyToSpawn;
        public GameObject enemyToSpawn2;
        private GameObject currentSpawn;
        private GameObject enemyTemp;
        private GameObject gameController = null;
        private GameController gameControllerScript = null;
        private GameObject fade = null;
        public int enemyCount = 0;
        private int roundCount = 0;
        public int wavesCount = 3;
        public bool waveStart = false;
        private GameObject waveCounter = null;
        private GameObject enemieCounter = null;
        private Text waveCounterText = null;
        private Text enemieCounterText = null;
        private FadeLife fadeScript = null;

        //-----------------------------------------------------------
        // Get components and Gameobject for script
        //-----------------------------------------------------------
        private void Start()
        {
            currentSpawn = enemyToSpawn;
            gameController = GameObject.FindGameObjectWithTag("GameController");
            gameControllerScript = gameController.GetComponent<GameController>();
            waveCounter = GameObject.FindGameObjectWithTag("WaveCounter");
            waveCounterText = waveCounter.GetComponent<Text>();
            enemieCounter = GameObject.FindGameObjectWithTag("EnemieCounter");
            enemieCounterText = enemieCounter.GetComponent<Text>();
            fade = GameObject.FindGameObjectWithTag("Fade");
            fadeScript = fade.GetComponent<FadeLife>();
            waveCounterText.gameObject.SetActive(false);
            enemieCounterText.gameObject.SetActive(false);
        }

        //-----------------------------------------------------------
        // When waves starts and their are no enemies , creates new 
        //  wave on enemies
        //-----------------------------------------------------------
        private void Update()
        {
            enemieCounterText.text = "Enemies " + enemyCount + "/" + spawnLocations.Length;
            if (waveStart && enemyCount == 0)
            {
                waveCounterText.gameObject.SetActive(true);
                enemieCounterText.gameObject.SetActive(true);
                wavesCount--;
                roundCount++;
                if (wavesCount > -1)
                {
                    Spawn();
                    if (wavesCount == 0)
                    {
                        waveCounterText.text = "Final Wave";
                    }
                    else
                    {
                        waveCounterText.text = "Wave" + " " + roundCount;
                    }
                }
                else
                {
                    if (fadeScript.startFadeOut == false)
                    {
                        fadeScript.startFadeOut = true;
                    }
                    if (fadeScript.fadeFinish)
                    {
                        gameControllerScript.currentSpawnLocation = 0;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                    }
                }
            }
        }

        //-----------------------------------------------------------
        // Spawns enemy to set spawnpoint
        //-----------------------------------------------------------
        public void Spawn()
        {
            foreach (Transform spawn in spawnLocations)
            {
                enemyTemp = Instantiate(currentSpawn, spawn.transform);
                enemyTemp.transform.parent = null;
                enemyCount++;
                if (currentSpawn == enemyToSpawn)
                {
                    currentSpawn = enemyToSpawn2;
                }
                else
                {
                    currentSpawn = enemyToSpawn;
                }
            }
        }
    }
}
