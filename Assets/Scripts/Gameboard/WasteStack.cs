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

        public int RevealedCardsStillInStack() => Reveal.Where(c => CardStack.Contains(c)).ToList().Count;

        public void RevealLastInStack(int numberOfCards)
        {
            Reveal.Clear();
            
            for (int i = numberOfCards; i > 0; i--)
            {
                PlayingCard card = CardStack[CardStack.Count - i];
                AddToRevealedCards(card);
            }
            
            ReorganizeExistingCards();
        }

        public void AddToRevealedCards(PlayingCard card) => Reveal.Add(card);

        public override void AssignPosition(PlayingCard card)
        {
            Vector3 targetPosition;
            
            targetPosition.x = transform.position.x;
            targetPosition.y = 
                IsInReveal(card) ? YPositionWithSpacing( Reveal.IndexOf(card)) : transform.position.y;
            targetPosition.z = ZPositionWithLayering(CardStack.IndexOf(card));

            card.MoveToPosition(targetPosition);
        }

        public Stack<CardInfo> GetRecycledStock()
        {
            Stack<CardInfo> newStack = new Stack<CardInfo>();
            for (int i = CardStack.Count - 1; i >= 0; i--)
            {
                newStack.Push(CardStack[i].CardInfo);
            }

            Clear();
            return newStack;
        }

        bool IsInReveal(PlayingCard card) => Reveal.Contains(card);

        void ReorganizeExistingCards()
        {
            foreach (PlayingCard card in CardStack) AssignPosition(card);
        }
    }
}