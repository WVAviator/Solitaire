using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Solitaire
{
    public class WinEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem hearts;
        [SerializeField] ParticleSystem diamonds;
        [SerializeField] ParticleSystem clubs;
        [SerializeField] ParticleSystem spades;
        
        [SerializeField] Sound winSound;

        void Awake()
        {
            DisableParticles();
            winSound.Source = gameObject.AddComponent<AudioSource>();
        }

        void OnEnable() => FoundationStack.OnAllFoundationsComplete += InitiateEffects;
        void OnDisable() => FoundationStack.OnAllFoundationsComplete -= InitiateEffects;
        
        void InitiateEffects(List<FoundationStack> foundationStacks)
        {
            OrientEffectsToSuit(foundationStacks);
            StartCoroutine(LaunchWinEffects());
            winSound.Play();
        }

        IEnumerator LaunchWinEffects()
        {
            EnableParticles();
            yield return new WaitForSeconds(4);
            DisableParticles();
        }

        void DisableParticles()
        {
            hearts.Stop();
            diamonds.Stop();
            clubs.Stop();
            spades.Stop();
        }

        void EnableParticles()
        {
            hearts.Play();
            diamonds.Play();
            clubs.Play();
            spades.Play();
        }

        void OrientEffectsToSuit(List<FoundationStack> foundationStacks)
        {
            foreach (FoundationStack foundation in foundationStacks)
            {
                if (foundation.GetFoundationSuit() == 0) hearts.transform.position = foundation.transform.position;
                if (foundation.GetFoundationSuit() == 1) diamonds.transform.position = foundation.transform.position;
                if (foundation.GetFoundationSuit() == 2) clubs.transform.position = foundation.transform.position;
                if (foundation.GetFoundationSuit() == 3) spades.transform.position = foundation.transform.position;
            }
        }
    }
}