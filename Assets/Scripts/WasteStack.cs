using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class WasteStack : Stack
    {
        public List<PlayingCard> Reveal { get; } = new List<PlayingCard>();

        public void ResetRevealedCards()
        {
            Reveal.Clear();
            ReorganizeExistingCards();
        }

        public int NumberOfRevealedCards() => Reveal.Where(c => PlayingCardsInStack.Contains(c)).ToList().Count;

        public void ResortReveal(int numberOfCards)
        {
            Reveal.Clear();
            
            for (int i = numberOfCards; i > 0; i--)
            {
                PlayingCard card = PlayingCardsInStack[PlayingCardsInStack.Count - i];
                AddToRevealedCards(card);
            }
            
            foreach (PlayingCard card in PlayingCardsInStack) card.UpdateCurrentStack(this);
        }

        public void AddToRevealedCards(PlayingCard card) => Reveal.Add(card);

        public override Vector3 GetPosition(PlayingCard card)
        {
            Vector3 targetPosition;
            
            targetPosition.x = transform.position.x;
            targetPosition.y = 
                IsInReveal(card) ? YPositionWithSpacing( Reveal.IndexOf(card)) : transform.position.y;
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

        bool IsInReveal(PlayingCard card) => Reveal.Contains(card);

        void ReorganizeExistingCards()
        {
            foreach (PlayingCard card in PlayingCardsInStack) card.UpdateCurrentStack(this);
        }
    }
}