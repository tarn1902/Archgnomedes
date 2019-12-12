using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    public class PlayerSoundControl : MonoBehaviour
    {
        private SoundManager soundManager;

        /// <summary>
        /// Weapon based sounds
        /// </summary>
        [Header("Weapon Sounds")]
        public AudioClip punchLaunch = null;
        public AudioClip punchRetract = null;
        public AudioClip punchImpact = null;
        public AudioClip shrinkRay = null;
        public AudioClip growthRay = null;

        /// <summary>
        /// Sounds of taking damage or a powerup
        /// </summary>
        [Header("Health Sounds")]
        public AudioClip watchSoundClip;
        public AudioClip whinySoundClip;
        public AudioClip hurtSoundClip;

        /// <summary>
        /// Sounds of Orcs being hit by the glove
        /// </summary>
        [Header("Punching Orc Sounds")]
        public AudioClip byeSoundClip;
        public AudioClip oppsSoundClip;
        public AudioClip cackleSoundClip;

        /// <summary>
        /// Sounds of the orcs being shrunk
        /// </summary>
        [Header("Shrinking Orc Sounds")]
        public AudioClip sizeSoundClip;
        public AudioClip shortSoundClip;
        public AudioClip seeyouSoundClip;

        /// <summary>
        /// Sounds of taking fatal damage
        /// </summary>
        [Header("Death Sounds")]
        public AudioClip wafptsSoundClip;
        public AudioClip croakSoundClip;
        public AudioClip unpleasantSoundClip;
        public AudioClip gnomeSoundClip;
        public AudioClip moresiSoundClip;
        public AudioClip gnomemoreSoundClip;


        public void Start()
        {
            soundManager = GetComponent<SoundManager>();
        }


        /// <summary>
        /// Sound for the glove launching
        /// </summary>
        public void PlayLaunch()
        {
            soundManager.PlayAudio("PunchLaunch");
        }


        /// <summary>
        /// Sound of the impact of the glove upon
        /// </summary>
        public void PlayImpact()
        {
            soundManager.PlayAudio("PunchHit");
        }

        public void PlayRay()
        {
            soundManager.PlayAudio("ShrinkHit");
        }

        /// <summary>
        /// A damage sound that plays for the audio
        /// </summary>
        public void PlayerDamageSound()
        {
            AudioClip[] array = new AudioClip[]
            {
                null,
                watchSoundClip,
                null,
                whinySoundClip,
                null,
                hurtSoundClip,
            };

            AudioClip clipToPlay;

            int randomind = Random.Range(0, array.Length);
            clipToPlay = array[randomind];

            soundManager.PlayAudio(clipToPlay.ToString());
            
        }

        public void PunchOrcSound()
        {
            AudioClip[] array = new AudioClip[]
            {
                sizeSoundClip,
                shortSoundClip,
                seeyouSoundClip
            };

            AudioClip clipToPlay;

            int randomind = Random.Range(0, array.Length);
            clipToPlay = array[randomind];

            soundManager.PlayAudio(clipToPlay.ToString());
        }


        public void PlayShrinkSound()
        {
            AudioClip[] array = new AudioClip[]
            {
                byeSoundClip,
                oppsSoundClip,
                cackleSoundClip
            };

            AudioClip clipToPlay;

            int randomind = Random.Range(0, array.Length);
            clipToPlay = array[randomind];

            soundManager.PlayAudio(clipToPlay.ToString());
        }

        public void PlayDeathSound()
        {
            AudioClip[] array = new AudioClip[]
            {
                wafptsSoundClip,
                croakSoundClip,
                unpleasantSoundClip,
                gnomeSoundClip,
                moresiSoundClip,
                gnomemoreSoundClip
            };

            AudioClip clipToPlay;

            int randomind = Random.Range(0, array.Length);
            clipToPlay = array[randomind];

            soundManager.PlayAudio(clipToPlay.ToString());
        }
    }
}