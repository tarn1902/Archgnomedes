using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    /// <summary>
    /// Class to play the audio sound for the shattering of objects
    /// This was implemented before the audio controller was implemented
    /// </summary>
    public class ObjectSoundControl : MonoBehaviour
    {
        private AudioSource audioSource = null;

        [Header("Sound Settings")]
        public AudioClip impact = null;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayImpact()
        {
            audioSource.PlayOneShot(impact);
        }
    }
}

