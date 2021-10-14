using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class CardSprites : MonoBehaviour
    {
        [Tooltip("Sprites in order A-K (Spades, Hearts, Clubs, Diamonds)")] 
        [SerializeField] List<Sprite> cardSprites;

        public static CardSprites Instance;
        void Awake() => Instance = this;
        
        public Sprite GetSprite(CardData card)
        {   
            return cardSprites[card.Index];
        }
    }
}