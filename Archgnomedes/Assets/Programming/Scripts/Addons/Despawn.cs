/*----------------------------------------
File Name: Despawn.cs
Purpose: Despawns game object
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class Despawn : MonoBehaviour
    {
        public bool hasLifeTime = false;
        public float lifeTime = 10;
        //-----------------------------------------------------------
        // Checks if any of conditions are met to despawn object
        //-----------------------------------------------------------
        void Update()
        {
            if (transform.position.y < -100)
            {
                Despawner();
            }
            if (hasLifeTime)
            {
                if (lifeTime < 0)
                {
                    Despawner();
                }
                else
                {
                    lifeTime -= Time.deltaTime;
                }
            }
        }
        //-----------------------------------------------------------
        // Destroys game object
        //-----------------------------------------------------------
        void Despawner()
        {
            Destroy(gameObject);
        }
    }
}
