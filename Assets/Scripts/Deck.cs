using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    [Serializable]
    public class Deck
    {
        readonly Stack<CardInfo> _cardStack;
        public Deck()
        {
            List<CardInfo> cards = BuildNewDeck();

            cards.Shuffle();
            _cardStack = new Stack<CardInfo>(cards);
        }

        public Deck(Stack<CardInfo> cardStack)
        {
            _cardStack = cardStack;
        }

        public CardInfo DrawCard()
        {
            if (CardsRemaining() != 0) return _cardStack.Pop();
            
            Debug.Log("Attempted to draw from an empty deck.");
            return new CardInfo(0, 0);
        }

        public int CardsRemaining() => _cardStack.Count;

        public void AddToStack(CardInfo card) => _cardStack.Push(card);

        static List<CardInfo> BuildNewDeck()
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

            return cards;
        }
    }
}