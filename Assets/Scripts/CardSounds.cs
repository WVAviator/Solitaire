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

        PlayingCard _playingCard;
        CardVisuals _cardVisuals;

        void Awake()
        {
            cardUp.Source = gameObject.AddComponent<AudioSource>();
            cardDown.Source = gameObject.AddComponent<AudioSource>();
            cardFlipped.Source = gameObject.AddComponent<AudioSource>();

            _playingCard = GetComponent<PlayingCard>();
            _cardVisuals = GetComponent<CardVisuals>();
        }

        void OnEnable()
        {
            _playingCard.OnCardPicked += PlayUpSound;
            _playingCard.OnCardPlaced += PlayDownSound;
            _cardVisuals.OnCardFlipped += PlayFlippedSound;
        }

        void OnDisable()
        {
            _playingCard.OnCardPicked -= PlayUpSound;
            _playingCard.OnCardPlaced -= PlayDownSound;
            _cardVisuals.OnCardFlipped -= PlayFlippedSound;
        }

        void PlayDownSound() => cardDown.Play();
        void PlayUpSound() => cardUp.Play();
        void PlayFlippedSound() => cardFlipped.Play();

    }
}