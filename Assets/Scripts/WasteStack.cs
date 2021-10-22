using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class WasteStack : Stack
    {
        int _cardsRemainingInReveal;
        int _cardsDrawnThisReveal;

        public void RevealCards(PlayingCard[] cards)
        {
            _cardsRemainingInReveal = cards.Length;
            _cardsDrawnThisReveal = _cardsRemainingInReveal;

            ReorganizeExistingCards();

            DrawNewCards(cards);
        }

        void DrawNewCards(PlayingCard[] cards)
        {
            foreach (PlayingCard c in cards)
            {
                AddCard(c);
            }
        }

        void ReorganizeExistingCards()
        {
            foreach (PlayingCard card in PlayingCardsInStack)
            {
                Vector3 newPosition;
                newPosition.x = transform.position.x;
                newPosition.y = transform.position.y;
                newPosition.z = ZPositionFromListPosition(card);
                
                card.MoveToPosition(newPosition, true);
            }
        }

        float ZPositionFromListPosition(PlayingCard card) => -0.01f * PlayingCardsInStack.IndexOf(card);

        protected override void SetPosition(PlayingCard card)
        {
            int positionInDraw = _cardsDrawnThisReveal - _cardsRemainingInReveal--;
            
            Vector3 targetPosition;
            targetPosition.x = transform.position.x;
            targetPosition.y = YPositionWithSpacing(positionInDraw);
            targetPosition.z = ZPositionWithLayering();

            card.MoveToPosition(targetPosition);
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
    }
}