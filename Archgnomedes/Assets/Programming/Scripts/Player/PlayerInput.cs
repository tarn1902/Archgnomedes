/*----------------------------------------
File Name: PlayerAction.cs
Purpose: Controls Inputs seperate from 
    player Controller
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archgnomedes
{
    public class PlayerInput : MonoBehaviour
    {
        [HideInInspector] public bool readyToFireSG = true;
        [HideInInspector] public bool readyToFirePB = true;

        [Header("Input settings")]
        public float rateOfChange = 0.01f;
        public bool useGlove = true;
        public GameObject glove = null;
        private Camera mainCam = null;
        private GameObject pauseMenu = null;
        private SizeGun sizegun = null;
        private PunchBox punchBox = null;
        private Quaternion pauseRotation;
        private bool isPaused = false;
        private void Start()
        {
            if (!useGlove)
            {
                glove.SetActive(false);
            }
            mainCam = Camera.main;
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenu.SetActive(false);
            sizegun = GetComponent<SizeGun>();
            punchBox = GetComponent<PunchBox>();

        }

        //-----------------------------------------------------------
        // Each update will check if these inputs have been pressed
        // and wait till is availble again.
        //-----------------------------------------------------------
        private void Update()
        {
            if (readyToFireSG)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    readyToFireSG = sizegun.Fire(0);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    readyToFireSG = sizegun.Fire(1);
                }
            }
            if (useGlove)
            {
                if (readyToFirePB)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        readyToFirePB = punchBox.Fire();
                    }
                }
                if (!glove.activeSelf)
                {
                    glove.SetActive(true);
                }
            }
            

            if (isPaused)
            {
                transform.rotation = pauseRotation;
                mainCam.enabled = true;
                isPaused = false;
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!isPaused)
                {
                    pauseRotation = transform.rotation;
                    mainCam.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                    isPaused = true;
                }
            }
        }

    }
}
