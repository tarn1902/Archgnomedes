/*----------------------------------------
File Name: PlayerLife.cs
Purpose: Changes lif points of player and
    displays it
Author: Tarn Cooper
Modified: 01/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class PlayerLife : MonoBehaviour
    {
        [Header("Crystal Settings")]
        public GameObject[] crystals;
        public Material glowMaterial;
        public Material noGlowMaterial;
        public GameObject brokenCrystal = null;

        [Header("Health Settings")]
        public int immortalTime = 10;
        public bool isDead = false;

        [Header("Sound Settings")]
        public AudioClip crystalBreak = null;
        public float crystalBreakVS = 1;
        public AudioClip crystalGain = null;
        public float crystalGainVS = 1;

        private int totalCrystalsIndex = 2;
        private int selectedCrystal = 0;
        private int previousCrystal = 0;
        private float currentTime = 10;
        private float startTime = 0;
        private int spliter = 2;
        private int halfSplit = 1;
        private AudioSource audioSource;
        private GameObject[] enemies;
        private GameObject tempCrystal = null;

        //-----------------------------------------------------------
        // Sets up crystals and audio
        //-----------------------------------------------------------
        private void Start()
        {
            foreach (GameObject crystal in crystals)
            {
                crystal.GetComponent<Renderer>().material = glowMaterial;
            }
            audioSource = GetComponent<AudioSource>();
        }
        //-----------------------------------------------------------
        // Player loses life
        //-----------------------------------------------------------
        public void LoseLife(Collision collision)
        {
            if (immortalTime < currentTime)
            {
                if (selectedCrystal != 3)
                {
                    currentTime = startTime;
                    tempCrystal = Instantiate(brokenCrystal, crystals[selectedCrystal].transform);
                    tempCrystal.transform.parent = crystals[selectedCrystal].transform.parent;
                    foreach (Rigidbody rb in tempCrystal.GetComponentsInChildren<Rigidbody>())
                    {
                        rb.AddForce(new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10)));
                    }
                    crystals[selectedCrystal].SetActive(false);
                    selectedCrystal++;
                }

            }
            if (selectedCrystal >= 3)
            {
                isDead = true;
            }
        }

        //-----------------------------------------------------------
        // Player loses life
        //-----------------------------------------------------------
        public void LoseLife(ControllerColliderHit collision)
        {
            
            if (immortalTime < currentTime)
            {
                if (selectedCrystal != 3)
                {
                    currentTime = startTime;
                    tempCrystal = Instantiate(brokenCrystal, crystals[selectedCrystal].transform);
                    tempCrystal.transform.parent = crystals[selectedCrystal].transform.parent;
                    foreach (Rigidbody rb in tempCrystal.GetComponentsInChildren<Rigidbody>())
                    {
                        rb.AddForce(new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10)));
                    }
                    crystals[selectedCrystal].SetActive(false);
                    selectedCrystal++;
                }
            }
            if (selectedCrystal == 3)
            {
                isDead = true;
            }
        }

        //-----------------------------------------------------------
        // Player gains life
        //-----------------------------------------------------------
        public bool GainLife()
        {
            if (selectedCrystal != 0)
            {
                crystals[selectedCrystal].SetActive(true);
                selectedCrystal--;
                previousCrystal = selectedCrystal;
                return true;
            }
            else
            {
                return false;
            }

        }

        //-----------------------------------------------------------
        // Increases Time
        //-----------------------------------------------------------
        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        //-----------------------------------------------------------
        // Unused. Blinks crystals
        //-----------------------------------------------------------
        void Blink()
        {
            if (previousCrystal != 3)
            {
                if (immortalTime > currentTime)
                {
                    if (currentTime % spliter > halfSplit)
                    {
                        crystals[previousCrystal].GetComponent<Renderer>().material = glowMaterial;
                    }
                    else
                    {
                        crystals[previousCrystal].GetComponent<Renderer>().material = noGlowMaterial;
                    }
                }
                else if (selectedCrystal > previousCrystal)
                {
                    crystals[previousCrystal].GetComponent<Renderer>().material = noGlowMaterial;
                    previousCrystal = selectedCrystal;
                }
            }
        }
    }
}
