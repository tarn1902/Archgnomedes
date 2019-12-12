using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    /// <summary>
    /// Make the blood splatter when the orc skeleton is spawned
    /// </summary>
    public class Blood : MonoBehaviour
    {
        public void BloodGone(GameObject blood)
        {
            Destroy(gameObject, 5f);
        }
        
    }
}
