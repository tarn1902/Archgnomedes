/*----------------------------------------
File Name: PlayerCollision.cs
Purpose: Checks if able to change size
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class PlayerCollision : MonoBehaviour
    {
        private Rigidbody rb = null;
        float nextCollisionTime = 2;
        float currentTime = 5;
        [Header("Physics Settings")]
        public float collisionForceRequirment = 10;
        public float pushPower = 2.0f;

        private void Update()
        {
            currentTime += Time.deltaTime;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (currentTime >= nextCollisionTime)
            {
                if (collision.transform.tag == "Object")
                {
                    if (collision.relativeVelocity.magnitude >= collisionForceRequirment)
                    {
                        currentTime = 0;
                        GetComponent<PlayerLife>().LoseLife(collision);
                        
                    }
                }
                else if (collision.transform.tag == "Weapon")
                {
                    currentTime = 0;
                    GetComponent<PlayerLife>().LoseLife(collision);
                    
                }
            }
            
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (currentTime >= nextCollisionTime)
            {
                if (hit.transform.tag == "Object")
                {
                    if (hit.rigidbody.velocity.magnitude >= collisionForceRequirment)
                    {
                        currentTime = 0;
                        GetComponent<PlayerLife>().LoseLife(hit);
                        
                    }
                }
                else if (hit.transform.tag == "Weapon")
                {
                    currentTime = 0;
                    GetComponent<PlayerLife>().LoseLife(hit);
                }

                
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.moveDirection * pushPower, ForceMode.Force);
            }


        }
    }
}
