/*----------------------------------------
File Name: CollectionTrigger.cs
Purpose: Triggers life increase
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class CollectionTrigger : MonoBehaviour
    {
        //-----------------------------------------------------------
        // gives life to player
        //  other (collider): gets collider info
        //-----------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerLife>().GainLife())
                {
                    SoundManager.instance.PlayAudio("HealthSound");
                    Destroy(gameObject);
                }
            }
        }
    }
}
