using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archgnomedes
{
    public class EnemySounds : MonoBehaviour
    {
        private SoundManager soundManager;

        /// <summary>
        /// AI related sounds
        /// </summary>
        [Header("AI Sounds")]
        public AudioClip enemyAlert;
        public AudioClip enemyDead;

        /// <summary>
        /// Movement related sounds
        /// </summary>
        [Header("Movement Sounds")]
        public AudioClip[] enemyFootsteps;

        /// <summary>
        /// Effects of being transformed
        /// </summary>
        [Header("Growing Sounds")]
        public AudioClip enemyGrown;
        public AudioClip enemyShrunk;



        private void Start()
        {
            soundManager = GetComponent<SoundManager>();
        }



        /// <summary>
        /// Enemy alerted soundfile
        /// </summary>
        public void EnemyAlertSound()
        {
            soundManager.PlayAudio(enemyAlert.ToString());
        }

        

        /// <summary>
        /// Enemy has been killed soundfile
        /// </summary>
        public void EnemyDeadSound()
        {
            soundManager.PlayAudio(enemyDead.ToString());
        }

        /// <summary>
        /// Enemy footsteps, played in an array and randomizing the pitch in the footsteps
        /// </summary>
        public void EnemyFootSound()
        {
            soundManager.RandomAudio(enemyFootsteps.ToString());
        }

        /// <summary>
        /// Enemy grown Soundfile
        /// </summary>
        public void EnemyGrownSound()
        {
            soundManager.PlayAudio(enemyGrown.ToString());
        }

        /// <summary>
        /// Enemy shrunk soundfile
        /// </summary>
        public void EnemyShrunkSound()
        {
            soundManager.PlayAudio(enemyShrunk.ToString());
        }

    }
}
