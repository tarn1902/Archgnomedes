using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A behaviour class for Audiofiles to apply fields for the
/// audiomanager class to apply the 
/// </summary>
namespace Archgnomedes
{
    [System.Serializable]

    
    public class AudioFile
    {
        public string audioName;

        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.5f, 1.5f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;

        public bool isLoop;
        public bool playOnAwake;
    }
}
