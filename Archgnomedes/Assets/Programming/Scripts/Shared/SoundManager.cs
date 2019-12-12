using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Archgnomedes
{
    /// <summary>
    /// Class to control the sound inside the game
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        /// <summary>
        /// Sound
        /// </summary>
        [Header("Sound Objects")]
        public AudioFile[] audioFiles;
        public AudioFile[] soundTrack;

        [Header("Sound Settings")]
        [Range(0f, 1f)]
        public float lowPitchRange = .90f;
        [Range(1, 1.25f)]
        public float highPitchRange = 1.1f;
        public float musicVolume;
        public float effectVolume;

        /// <summary>
        /// Make the soundmanager class when first made.
        /// Also allows the soundmanager to not be destroyed upon reloading the scene
        /// </summary>
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            foreach (var clip in audioFiles)
            {
                clip.source = gameObject.AddComponent<AudioSource>();
                clip.source.clip = clip.audioClip;
                clip.source.volume = clip.volume;
                clip.source.loop = clip.isLoop;

                if(clip.playOnAwake)
                {
                    clip.source.Play();
                }
            }

            musicVolume = PlayerPrefs.GetFloat("MusicVolume", .75f);
            effectVolume = PlayerPrefs.GetFloat("EffectsVolume", .75f);

            // Create the sound files in an array, than create the audio files in the soundtrack
            CreateAudio(audioFiles, effectVolume);
            CreateAudio(soundTrack, musicVolume);

        }


        private void CreateAudio(AudioFile[] audio, float volume)
        {
            foreach (AudioFile clips in audioFiles)
            {
                clips.source.clip = clips.audioClip;
                clips.source.volume = clips.volume * volume;
                clips.source.loop = clips.isLoop;
            }
        }

        /// <summary>
        /// Music to play
        /// </summary>
        /// <param name="name"></param>
        public static void MusicPlay(string name)
        {
            AudioFile file = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);

            if(file == null)
            {
                Debug.LogError("Sound name " + name + "not found!");
                return;
            }
            else
            {
                file.source.Play();
            }
        }


        /// <summary>
        /// Music to be stopped
        /// </summary>
        /// <param name="name"></param>
        public static void StopMusic(string name)
        {
            AudioFile clip = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);

            if(clip == null)
            {
                Debug.LogError("Sound name" + name + " not found!");
                return;
            }

            else
            {
                clip.source.Stop();
            }
        }


        /// <summary>
        /// Pause music
        /// </summary>
        /// <param name="name"></param>
        public static void PauseMusic(string name)
        {
            AudioFile clip = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);

            if(clip == null)
            {
                Debug.LogError("Sound name " + name + " not found!");
                return;
            }

            else
            {
                clip.source.Pause();
            }
        }

        
        /// <summary>
        /// Unpause music to play
        /// </summary>
        /// <param name="name"></param>
        public static void UnpauseMusic(string name)
        {
            AudioFile clip = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);

            if (clip == null)
            {
                Debug.LogError("Sound name " + name + " not found!");
                return;
            }

            else
            {
                clip.source.Pause();
            }
        }



        /// <summary>
        /// Plays the sound that it needed for the audio
        /// </summary>
        public void PlayAudio(string name)
        {
            AudioFile clip = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
            if(clip  == null)
            {
                Debug.LogError("Unable to play sound!" + name);
                return;
            }

            clip.source.Play();
        }




        /// <summary>
        /// Plays a random sound file, with an altered pitch
        /// </summary>
        /// <param name="clips"></param>
        public void RandomAudio(string name)
        {
            AudioFile clip = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);

            if(clip == null)
            {
                Debug.LogError("Unable to play sound!" + name);
                return;
            }

            clip.source.Play();
        }
    }
}
