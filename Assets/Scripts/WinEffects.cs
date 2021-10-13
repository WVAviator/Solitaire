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

        [SerializeField] Foundation[] upperStacks;

        void Awake()
        {
            DisableParticles();
            GameManager.OnGameWin += InitiateEffects;
        }

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
            foreach (Foundation up in upperStacks)
            {
                if (up.GetActiveSuit() == 0) hearts.transform.position = up.transform.position;
                if (up.GetActiveSuit() == 1) diamonds.transform.position = up.transform.position;
                if (up.GetActiveSuit() == 2) clubs.transform.position = up.transform.position;
                if (up.GetActiveSuit() == 3) spades.transform.position = up.transform.position;
            }
        }
    }
}