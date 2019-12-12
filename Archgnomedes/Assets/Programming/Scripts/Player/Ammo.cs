/*----------------------------------------
File Name: Ammo.cs
Purpose: Scales vials in set direction
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class Ammo : MonoBehaviour
    {
        [Header("Enegy Settings")]
        public int maxEnergy = 10;
        public GameObject growVial = null;
        public GameObject shrinkVial = null;

        private Vector3 newShrinkScale;
        private Vector3 newGrowScale;
        private Vector3 currentGrowScale;
        private Vector3 currentShrinkScale;
        private Vector3 updatedGrowScale;
        private Vector3 updatedShrinkScale;
        private int growEnergy = 0;
        private int shrinkEnergy = 0;

        private float scaler = 0;
        private float currentChange = 0;
        private float rateOfChange = 0;
        private GameObject player;
        private PlayerInput input = null;

        private bool isChanging = false;

        //-----------------------------------------------------------
        // sets up values to be used
        //-----------------------------------------------------------
        void Start()
        {
            growEnergy = maxEnergy / 2;
            shrinkEnergy = maxEnergy / 2;
            newGrowScale = Vector3.zero;
            newShrinkScale = Vector3.zero;
            currentGrowScale = Vector3.zero;
            currentShrinkScale = Vector3.zero;
            scaler = growVial.transform.localScale.y / growEnergy;
        }

        //-----------------------------------------------------------
        // Changes ammount depending on sizing direction
        // Return (bool): retruns if have ammo to do change
        //-----------------------------------------------------------
        public bool ChangeAmmo(int sizeDirection)
        {
            if (sizeDirection == 1)
            {
                if (growEnergy != 0)
                {
                    return GrowToShrink();
                }
                else
                {
                    return false;
                }
            }
            else if (sizeDirection == 0)
            {
                if (shrinkEnergy != 0)
                {
                    return ShrinkToGrow();
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //-----------------------------------------------------------
        // Changes vials each frame till at target amount
        //-----------------------------------------------------------
        private void Update()
        {
            if (input == null)
            {
                input = GetComponent<PlayerInput>();
            }
            else
            {
                if (rateOfChange == 0)
                {
                    rateOfChange = input.rateOfChange;
                }
                else
                {
                    if (isChanging)
                    {
                        updatedGrowScale.x = growVial.transform.localScale.x;
                        updatedGrowScale.y = Mathf.Lerp(currentGrowScale.y, newGrowScale.y, currentChange);
                        updatedGrowScale.z = growVial.transform.localScale.z;

                        updatedShrinkScale.x = shrinkVial.transform.localScale.x;
                        updatedShrinkScale.y = Mathf.Lerp(currentShrinkScale.y, newShrinkScale.y, currentChange);
                        updatedShrinkScale.z = shrinkVial.transform.localScale.z;

                        growVial.transform.localScale = updatedGrowScale;
                        shrinkVial.transform.localScale = updatedShrinkScale;


                        currentChange += rateOfChange;
                    }
                    if (currentChange >= 1.1f)
                    {
                        currentChange = 0;
                        isChanging = false;
                    }
                }
            }
        }

        //-----------------------------------------------------------
        // Transfers shrink fluid to grow sa
        //-----------------------------------------------------------
        bool ShrinkToGrow()
        {
            shrinkEnergy--;
            growEnergy++;
            currentGrowScale = growVial.transform.localScale;
            currentShrinkScale = shrinkVial.transform.localScale;
            newShrinkScale.y = currentShrinkScale.y - scaler;
            newGrowScale.y = currentGrowScale.y + scaler;
            isChanging = true;
            return true;
        }
        //-----------------------------------------------------------
        // Transfers grow fluid to shrink
        //-----------------------------------------------------------
        bool GrowToShrink()
        {
            shrinkEnergy++;
            growEnergy--;
            currentGrowScale = growVial.transform.localScale;
            currentShrinkScale = shrinkVial.transform.localScale;
            newShrinkScale.y = currentShrinkScale.y + scaler;
            newGrowScale.y = currentGrowScale.y - scaler;
            isChanging = true;
            return true;
        }
    }
}
