using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    /// <summary>
    /// Class to implement the fire sound. Didn't make the final solution
    /// Due to the audio manager implementation
    /// </summary>
    public class FireSound : MonoBehaviour
    {
        private SoundManager soundManager;

        [Header("Fire Sounds")]
        public AudioClip crackleSound;

        public void Start()
        {
            soundManager = GetComponent<SoundManager>();
        }
    }
}
