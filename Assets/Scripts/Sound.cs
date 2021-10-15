using System;
using UnityEngine;

namespace Solitaire
{
    [Serializable]
    public class Sound
    {
        AudioSource _audioSource;

        [SerializeField] AudioClip clip;

        [Range(0f,1f)]
        [SerializeField] float volume = 0.5f;
        [Range(0.1f, 3)]
        [SerializeField] float pitch = 1;
        
        public AudioSource Source
        {
            set
            {
                _audioSource = value;
                _audioSource.clip = clip;
                _audioSource.volume = volume;
                _audioSource.pitch = pitch;
            } 
        }

        public void Play() => _audioSource.Play();

    }
}