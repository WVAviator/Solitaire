using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class Deck
    {
        readonly Stack<CardInfo> cardStack;
        public Deck()
        {
            List<CardInfo> cards = new List<CardInfo>();
            for (int r = 0; r < 13; r++)
            {
                for (int s = 0; s < 4; s++)
                {
                    CardInfo newCard = new CardInfo(s, r);
                    cards.Add(newCard);
                }
            }
            
            cards.Shuffle();
            cardStack = new Stack<CardInfo>(cards);
        }

        public Deck(Stack<CardInfo> cardStack)
        {
            this.cardStack = cardStack;
        }

        public CardInfo DrawCard()
        {
            if (CardsRemaining() == 0)
            {
                Debug.Log("Attempted to draw from an empty deck.");
                return new CardInfo(0, 0);
            }
            return cardStack.Pop();
        }

        public int CardsRemaining() => cardStack.Count;
        
    }
}