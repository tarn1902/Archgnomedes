/*----------------------------------------
File Name: WaveTrigger.cs
Purpose: Triggers wave spawn
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class WaveTrigger : MonoBehaviour
    {
        //-----------------------------------------------------------
        // turns wave on
        //  other (collider): gets collider info
        //-----------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawn>().waveStart = true;
            }
        }
    }
}
