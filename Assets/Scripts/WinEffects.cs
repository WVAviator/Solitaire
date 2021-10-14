using System;
using System.Collections;
using UnityEngine;

namespace Solitaire
{
    public class WinEffects : MonoBehaviour
    {
        [SerializeField] ParticleSystem hearts;
        [SerializeField] ParticleSystem diamonds;
        [SerializeField] ParticleSystem clubs;
        [SerializeField] ParticleSystem spades;
        
        void Awake() => DisableParticles();
        void OnEnable() => GameManager.OnGameWin += InitiateEffects;
        void OnDisable() => GameManager.OnGameWin -= InitiateEffects;
        
        void InitiateEffects()
        {
            OrientEffectsToSuit();
            StartCoroutine(LaunchWinEffects());
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

        void OrientEffectsToSuit()
        {
            foreach (FoundationStack f in GameBoard.Instance.Foundations)
            {
                if (f.GetFoundationSuit() == 0) hearts.transform.position = f.transform.position;
                if (f.GetFoundationSuit() == 1) diamonds.transform.position = f.transform.position;
                if (f.GetFoundationSuit() == 2) clubs.transform.position = f.transform.position;
                if (f.GetFoundationSuit() == 3) spades.transform.position = f.transform.position;
            }
        }
    }
}