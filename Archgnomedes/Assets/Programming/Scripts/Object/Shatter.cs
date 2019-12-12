/*----------------------------------------
File Name: ShatterAction.cs
Purpose: Loses durability till shatters
Author: Tarn Cooper
Modified: 01/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class Shatter : MonoBehaviour
    {
        [Header("Objects")]
        public GameObject shatteredPieces = null;

        [Header("Force Interactions")]
        public float minForce = 1;
        public int maxForce = 5;

        [Header("Life")]
        public int durability = 3;

        private ObjectSoundControl soundControl = null;
        private SizeChange sizeControl = null;
        private CollectibleSpawn collectibleSpawn = null;
        private float randomMultiplier = 0;

        //-----------------------------------------------------------
        // gets component related to size
        //-----------------------------------------------------------
        private void Start()
        {
            soundControl = GetComponent<ObjectSoundControl>();
            sizeControl = GetComponent<SizeChange>();
            collectibleSpawn = GetComponent<CollectibleSpawn>();
        }
        //-----------------------------------------------------------
        // will lose life then spawn in destroyed version with force 
        // of collision
        // collision (Collision): Gets collision infomation
        //-----------------------------------------------------------
        public void Shatterer(Collision collision)
        {
            if (sizeControl.originalSize < sizeControl.currentSize)
            {
                durability -= 1;
            }
            else if (sizeControl.originalSize == sizeControl.currentSize)
            {
                durability -= 2;
            }
            else if (sizeControl.originalSize > sizeControl.currentSize)
            {
                durability -= 3;
            }

            if (durability <= 0)
            {
                if (collectibleSpawn != null)
                {
                    collectibleSpawn.SpawnCollectible();
                }
                shatteredPieces = Instantiate(shatteredPieces, transform.position, transform.rotation);
                shatteredPieces.transform.localScale = sizeControl.sizes[sizeControl.currentSize];
                if (collision.transform.tag == "Glove")
                {
                    foreach (Rigidbody part in shatteredPieces.GetComponentsInChildren<Rigidbody>())
                    {
                        randomMultiplier = Random.Range(minForce, maxForce);
                        part.AddForce(collision.transform.right * randomMultiplier, ForceMode.Impulse);
                    }
                }
                else
                {
                    foreach (Rigidbody part in shatteredPieces.GetComponentsInChildren<Rigidbody>())
                    {
                        randomMultiplier = Random.Range(minForce, maxForce);
                        part.AddForce(collision.relativeVelocity.normalized * randomMultiplier, ForceMode.Impulse);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
