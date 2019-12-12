/*----------------------------------------
File Name: CheckpointTrigger.cs
Purpose: Triggers checkpoint change
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class CheckpointTrigger : MonoBehaviour
    {
        [Header("Spawn Selection Settings")]
        public int spawnPoint;

        //-----------------------------------------------------------
        // changes spawn location to checkpoint
        //  other (collider): gets collider info
        //-----------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().currentSpawnLocation = spawnPoint;
            }
            
        }
    }
}
