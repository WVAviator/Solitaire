using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class CardManager : MonoBehaviour
    {
        [Tooltip("Sprites in order A-K (Spades, Hearts, Clubs, Diamonds)")] [SerializeField]
        List<Sprite> cardSprites;

        public static CardManager Instance;

        void Awake() => Instance = this;

        Sprite GetSprite(int cardIndex)
        {
            return cardSprites[cardIndex];
        }

        public Sprite GetSprite(Card card)
        {
            return GetSprite(card.GetCardIndex());
        }
    }
}