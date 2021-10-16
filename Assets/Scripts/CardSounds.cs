using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;

namespace Solitaire
{
    public class CardSounds : MonoBehaviour
    {

        [SerializeField] Sound cardUp;
        [SerializeField] Sound cardDown;
        [SerializeField] Sound cardFlipped;

        PlayingCard playingCard;

        void Awake()
        {
            cardUp.Source = gameObject.AddComponent<AudioSource>();
            cardDown.Source = gameObject.AddComponent<AudioSource>();
            cardFlipped.Source = gameObject.AddComponent<AudioSource>();

            playingCard = GetComponent<PlayingCard>();
        }

        void OnEnable()
        {
            playingCard.OnCardPicked += PlayUpSound;
            playingCard.OnCardPlaced += PlayDownSound;
            playingCard.OnCardFlipped += PlayFlippedSound;
        }

        void OnDisable()
        {
            playingCard.OnCardPicked -= PlayUpSound;
            playingCard.OnCardPlaced -= PlayDownSound;
            playingCard.OnCardFlipped -= PlayFlippedSound;
        }

        void PlayDownSound() => cardDown.Play();
        void PlayUpSound() => cardUp.Play();
        void PlayFlippedSound() => cardFlipped.Play();

    }
}