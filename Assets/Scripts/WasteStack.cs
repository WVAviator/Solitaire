using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class WasteStack : Stack
    {
        List<PlayingCard> _reveal = new List<PlayingCard>();

        public void ResetRevealedCards()
        {
            _reveal.Clear();
            ReorganizeExistingCards();
        }

        public void AddToRevealedCards(PlayingCard card) => _reveal.Add(card);

        public override Vector3 GetPosition(PlayingCard card)
        {
            Vector3 targetPosition;
            
            targetPosition.x = transform.position.x;
            targetPosition.y = 
                IsInReveal(card) ? YPositionWithSpacing( _reveal.IndexOf(card)) : transform.position.y;
            targetPosition.z = ZPositionWithLayering(PlayingCardsInStack.IndexOf(card));

            return targetPosition;
        }

        public Stack<CardInfo> GetRecycledStock()
        {
            Stack<CardInfo> newStack = new Stack<CardInfo>();
            for (int i = PlayingCardsInStack.Count - 1; i >= 0; i--)
            {
                newStack.Push(PlayingCardsInStack[i].CardInfo);
            }

            Clear();
            return newStack;
        }

        bool IsInReveal(PlayingCard card) => _reveal.Contains(card);

        void ReorganizeExistingCards()
        {
            foreach (PlayingCard card in PlayingCardsInStack) card.UpdateCurrentStack(this);
        }
    }
}