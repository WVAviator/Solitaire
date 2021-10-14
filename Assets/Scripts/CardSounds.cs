using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;

public class CardSounds : MonoBehaviour
{

    [SerializeField] Sound cardUp;
    [SerializeField] Sound cardDown;
    
    PlayingCard playingCard;

    void Awake()
    {
        cardUp.Source = gameObject.AddComponent<AudioSource>();
        cardDown.Source = gameObject.AddComponent<AudioSource>();
        
        playingCard = GetComponent<PlayingCard>();
    }
    
    void OnEnable()
    {
        playingCard.OnCardPicked += PlayUpSound;
        playingCard.OnCardPlaced += PlayDownSound;
    }
    void OnDisable()
    {
        playingCard.OnCardPicked -= PlayUpSound;
        playingCard.OnCardPlaced -= PlayDownSound;
    }
    
    void PlayDownSound() => cardDown.Play();
    void PlayUpSound() => cardUp.Play();
    
}
