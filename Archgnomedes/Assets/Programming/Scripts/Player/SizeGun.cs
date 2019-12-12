/*----------------------------------------
File Name: SizeGun.cs
Purpose: Checks if able to change size
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using System.Collections;
namespace Archgnomedes
{
    public class SizeGun : MonoBehaviour
    {

        [Header("Action Settings")]
        public GameObject firePoint;
        public int delayTime = 10;

        [Header("Laser Beam")]
        public float laserWidth = .1f;
        public float noise = 1f;
        public float maxLength = 10f;

        public Color shrinkColour = Color.red;
        public Color maxColour = Color.blue;
        public ParticleSystem laserBeam = null;
        public LineRenderer lineRend = null;
        
        int lengthOfRay;
        Vector3[] position;
        Transform myTransform;
        Transform endTransform;
        Vector3 ourOffset;

        private AudioSource audioSource;
        private RaycastHit hit;
        private Ammo ammoControl;
        private PlayerInput inputControl;

        private int currentTime = 0;
        private bool shootingBeam = false;
        public bool includeChildren = true;
        public GameObject shrinkVial = null;
        public GameObject growVial = null;
        public Material shrinkVialColour = null;
        public Material growVialColour = null;
        private Material shrinkOriginalColour = null;
        private Material growOriginalColour = null;
        private Camera mainCam = null;

        PlayerSoundControl soundControl = null;
        SoundManager soundManager;

        //-----------------------------------------------------------
        // Sets up components to use
        // 4/11, added Particle system to start command - PJ
        //-----------------------------------------------------------
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            soundManager = GetComponent<SoundManager>();
            ammoControl = GetComponent<Ammo>();
            inputControl = GetComponent<PlayerInput>();
            soundControl = GetComponent<PlayerSoundControl>();
            lineRend.startWidth = laserWidth;
            lineRend.endWidth = laserWidth;
            myTransform = firePoint.transform;
            ourOffset = new Vector3(0, 0, 0);
            laserBeam = GetComponentInChildren<ParticleSystem>();
            if (laserBeam)
                endTransform = laserBeam.transform;
            shrinkOriginalColour = shrinkVial.GetComponent<MeshRenderer>().sharedMaterial;
            growOriginalColour = growVial.GetComponent<MeshRenderer>().sharedMaterial;
            mainCam = Camera.main;
        }

        //-----------------------------------------------------------
        // If tiggered will keep a line on target till stops changing 
        // size
        //-----------------------------------------------------------
        private void Update()
        {
            if (shootingBeam)
            {
                if (hit.transform != null)
                {
                    if (!hit.transform.GetComponent<SizeChange>().isChangingSize || lineRend == null)
                    {
                        lineRend.enabled = false;
                        shootingBeam = false;
                        inputControl.readyToFireSG = true;
                        growVial.GetComponent<MeshRenderer>().sharedMaterial = growOriginalColour;
                        shrinkVial.GetComponent<MeshRenderer>().sharedMaterial = shrinkOriginalColour;
                    }
                    else
                    {
                        lineRend.SetPositions(new Vector3[] { firePoint.transform.position, hit.transform.position });
                    }

                }
                else
                {
                    lineRend.enabled = false;
                    shootingBeam = false;
                    inputControl.readyToFireSG = true;
                }

            }
        }

        //-----------------------------------------------------------
        // Tests if able to change size of target
        // sizeDirection (int): selects which direction of size 
        // change (0-1)
        // return (bool): Returns if able to fire again or not
        //-----------------------------------------------------------
        public bool Fire(int sizeDirection)
        {
            if (Physics.Raycast(new Ray(mainCam.transform.position, mainCam.transform.forward), out hit, 100, 1 << 9 | 1 << 10))
            {
                if (hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "Object")
                {
                    if (hit.transform.GetComponent<SizeChange>().Sizeable(sizeDirection))
                    {
                        if (ammoControl.ChangeAmmo(sizeDirection))
                        {
                            ShootBeam();
                            if (sizeDirection == 0)
                            {
                                shrinkVial.GetComponent<MeshRenderer>().sharedMaterial = shrinkVialColour;
                                lineRend.material = shrinkVialColour;
                                SoundManager.instance.PlayAudio("ShrinkHit");
                            }
                            else
                            {
                                growVial.GetComponent<MeshRenderer>().sharedMaterial = growVialColour;
                                lineRend.material = growVialColour;
                                SoundManager.instance.PlayAudio("GrowthHit");
                            }

                            hit.transform.gameObject.GetComponent<SizeChange>().ChangeSize(sizeDirection);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //-----------------------------------------------------------
        // If was to charge some how this audio will be played
        //-----------------------------------------------------------
        public void Chargeup()
        {
            //audioSource.PlayOneShot(chargeRay, chargeRayVS);
        }

        //-----------------------------------------------------------
        // Turns on and sets inital postion of line?
        // 4/11, changed the line renderer to particle system for laser effect - PJ
        //-----------------------------------------------------------
        private void ShootBeam()
        {
            lineRend.SetPositions(new Vector3[] { firePoint.transform.position, hit.transform.position });
            lineRend.enabled = true;
            shootingBeam = true;
        }


        //-----------------------------------------------------------
        // Emit the laser beam
        //-----------------------------------------------------------
        void LaserRender()
        {

            // Get the inputs, and change the correct colours
            if(Input.GetMouseButton(0))
            {
                lineRend.startColor = shrinkColour;
                lineRend.endColor = shrinkColour;
            }

            if(Input.GetMouseButton(1))
            {
                lineRend.startColor = maxColour;
                lineRend.startColor = maxColour;
            }

            // Move through the array, set the position to the current position and move the laser forward
            for(int i = 0; i < lengthOfRay; i++)
            {
                ourOffset.x = myTransform.position.x + i * myTransform.forward.x + Random.Range(-noise, noise);
                ourOffset.z = i * myTransform.forward.z + Random.Range(-noise, noise) + myTransform.position.z;
                position[i] = ourOffset;
                position[0] = myTransform.position;

                lineRend.SetPosition(i, position[i]);
            }
        }
    }
}
