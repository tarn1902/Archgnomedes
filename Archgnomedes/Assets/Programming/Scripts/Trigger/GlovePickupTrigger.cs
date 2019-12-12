/*----------------------------------------
File Name: GlovePickupTrigger.cs
Purpose: Triggers life increase
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{

    public class GlovePickupTrigger : MonoBehaviour
    {
        //-----------------------------------------------------------
        // enables glove for player
        //  other (collider): gets collider info
        //-----------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerInput>().useGlove = true;
                Destroy(gameObject);
            }
        }
    }
}
