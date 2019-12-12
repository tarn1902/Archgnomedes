/*----------------------------------------
File Name: ObjectCollision.cs
Purpose: controls collisions of object
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class ObjectCollision : MonoBehaviour
    {
        private Rigidbody rb = null;
        private Shatter shatter = null;

        [Header("Physics Settings")]
        public float collisionForceRequirment = 10;
        public float gloveForce = 10;

        //-----------------------------------------------------------
        // Gets Components
        //-----------------------------------------------------------
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            shatter = GetComponent<Shatter>();
        }

        //-----------------------------------------------------------
        // Reaction will happenof depending on game object collision
        //  collision(Collision) : gets collision infomation
        //-----------------------------------------------------------
        private void OnCollisionEnter(Collision collision)
        {
                if (collision.relativeVelocity.magnitude >= collisionForceRequirment)
                {
                    shatter.Shatterer(collision);
                }
            else if (collision.transform.tag == "Glove")
            {
                rb.AddForce(collision.transform.right * gloveForce, ForceMode.Impulse);

                shatter.Shatterer(collision);
                
            }
        }
    }
}
