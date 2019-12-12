/*----------------------------------------
File Name: Float.cs
Purpose: Despawns game object
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class Float : MonoBehaviour
    {
        public bool onlyRotateY = false;
        private float originalY;
        public float scale = 0.5f;
        public float rotationSpeed = 1;
        //-----------------------------------------------------------
        // gets Y position
        //-----------------------------------------------------------
        private void Start()
        {
            originalY = transform.position.y;
        }
        //-----------------------------------------------------------
        // floats object up and down, rotates if enables
        //-----------------------------------------------------------
        void Update()
        {
            transform.position = new Vector3(transform.position.x, originalY + (Mathf.Sin(Time.timeSinceLevelLoad) + 1) * scale, transform.position.z);
            if (onlyRotateY)
            {
                transform.Rotate(new Vector3(0, rotationSpeed, 0));
            }
            else
            {
                transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed));
            }
            
        }
    }
}
