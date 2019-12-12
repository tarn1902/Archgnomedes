using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    public class GloveCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PunchBox>().collided = true;
        }
    }
}

