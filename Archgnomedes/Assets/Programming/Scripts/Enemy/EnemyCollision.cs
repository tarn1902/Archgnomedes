/*----------------------------------------
File Name: EnemyCollision.cs
Purpose: Creates blood on collision
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class EnemyCollision : MonoBehaviour
    {
        private Rigidbody rb = null;
        private EnemyLife enemyLife = null;
        private Animator anim = null;
        private EnemyAI aI = null;
        private SizeChange sizeControl = null;

        [Header("Physics Settings")]
        public float collisionForceRequirment = 10;
        public float gloveForce = 10;
        public float immunityTime = 2;
        public float currentTime = 2;

        //-----------------------------------------------------------
        // Gets Components
        //-----------------------------------------------------------
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            enemyLife = GetComponent<EnemyLife>();
            anim = GetComponent<Animator>();
            aI = GetComponent<EnemyAI>();
            sizeControl = GetComponent<SizeChange>();
        }

        //-----------------------------------------------------------
        // Increase current time
        //-----------------------------------------------------------
        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        //-----------------------------------------------------------
        // Effects enemy depending on what enemy collided with
        //  collision (Collision): gets collision infomation
        //-----------------------------------------------------------
        private void OnCollisionEnter(Collision collision)
        {
            if (currentTime > immunityTime)
            {
                if (collision.transform.tag == "Object")
                {
                    if (collision.relativeVelocity.magnitude >= collisionForceRequirment)
                    {
                        currentTime = 0;
                        enemyLife.TakeLife(collision);
                        anim.SetBool("isAlerted", true);
                        aI.alertedTarget = collision.transform;
                    }

                }
                else if (collision.transform.tag == "Glove")
                {
                    if (sizeControl.currentSize == 0)
                    {
                        rb.AddForce(collision.transform.right * gloveForce, ForceMode.Impulse);
                        enemyLife.TakeLife(collision);
                        currentTime = 0;
                    }
                    else if (sizeControl.currentSize == 1)
                    {
                        anim.SetTrigger("Stun");
                    }
                    anim.SetBool("isAlerted", true);
                    aI.alertedTarget = collision.transform;
                }
            }
        }
    }
}
