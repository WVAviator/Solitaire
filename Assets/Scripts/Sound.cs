using System;
using UnityEngine;

namespace Solitaire
{
    [Serializable]
    public class Sound
    {
        AudioSource audioSource;

        [SerializeField] AudioClip clip;

        [Range(0f,1f)]
        [SerializeField] float volume = 0.5f;
        [Range(0.1f, 3)]
        [SerializeField] float pitch = 1;
        
        public AudioSource Source
        {
            set
            {
                audioSource = value;
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.pitch = pitch;
            } 
        }

        public void Play() => audioSource.Play();

    }
}