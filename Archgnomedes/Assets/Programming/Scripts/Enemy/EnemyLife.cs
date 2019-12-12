/*----------------------------------------
File Name: EnemyLife.cs
Purpose: Loses life till dead
Author: Tarn Cooper
Modified: 31/10/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using UnityEngine.AI;

namespace Archgnomedes
{
    public class EnemyLife : MonoBehaviour
    {
        [Header("Life Settings")]
        public int lifeCount = 3;
        public bool isWaveTrigger = false;
        public GameObject deadBody = null;
        public GameObject blood = null;
        public float force = 10;
        private float randomMultiplier = 0;
        public GameObject spawner = null;
        public float minForce = 1;
        public int maxForce = 5;

        private SizeChange sizeControl = null;
        private NavMeshAgent navMeshAgent = null;
        private Animator anim = null;
        private EnemySpawn enemySpawn = null;

        Blood bloodScript;
        //-----------------------------------------------------------
        // gets component related to size
        //-----------------------------------------------------------
        private void Start()
        {
            sizeControl = GetComponent<SizeChange>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            spawner = GameObject.FindGameObjectWithTag("Spawner");
            enemySpawn = spawner.GetComponent<EnemySpawn>();
        }

        //-----------------------------------------------------------
        // Loses life till has none, enemy then does nothing
        //  collision (Collision): gets collision infomation
        //-----------------------------------------------------------
        public void TakeLife(Collision collision)
        {
            if (sizeControl.originalSize < sizeControl.currentSize)
            {
                lifeCount -= 0;
            }
            else if (sizeControl.originalSize == sizeControl.currentSize)
            {
                if (collision.transform.GetComponent<SizeChange>().currentSize > 1)
                {
                    lifeCount -= lifeCount;
                }
                else
                {
                    lifeCount -= 1;
                }
            }
            else if (sizeControl.originalSize > sizeControl.currentSize)
            {
                lifeCount -= lifeCount;
            }

            if (lifeCount <= 0)
            {
                navMeshAgent.enabled = false;
                anim.SetTrigger("Die");
                anim.SetBool("isDead", true);
                deadBody = Instantiate(deadBody, transform);
                deadBody.transform.parent = null;
                blood = Instantiate(blood, transform);
                blood.transform.parent = null;

                
                if (isWaveTrigger)
                {
                    if (sizeControl.originalSize <= sizeControl.currentSize)
                    {
                        enemySpawn.enemyCount--;
                    }
                    
                }
                if (collision.transform.tag == "Glove")
                {
                    foreach (Rigidbody part in deadBody.GetComponentsInChildren<Rigidbody>())
                    {
                        randomMultiplier = Random.Range(minForce, maxForce);
                        part.AddForce(collision.transform.right * randomMultiplier, ForceMode.Impulse);
                    }
                }
                else
                {
                    foreach (Rigidbody part in deadBody.GetComponentsInChildren<Rigidbody>())
                    {
                        randomMultiplier = Random.Range(minForce, maxForce);
                        part.AddForce((transform.position - collision.transform.position).normalized * randomMultiplier, ForceMode.Impulse);
                    }
                }
                Destroy(gameObject);
            }   
        }
    }
}
