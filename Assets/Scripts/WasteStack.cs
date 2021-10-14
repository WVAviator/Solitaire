using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class WasteStack : Stack
    {
        int cardsRemainingInReveal;
        int cardsDrawnThisReveal;

        public void RevealCards(PlayingCard[] cards)
        {
            cardsRemainingInReveal = cards.Length;
            cardsDrawnThisReveal = cardsRemainingInReveal;

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
            int positionInDraw = cardsDrawnThisReveal - cardsRemainingInReveal--;
            
            Vector3 targetPosition;
            targetPosition.x = transform.position.x;
            targetPosition.y = YPositionWithSpacing(positionInDraw);
            targetPosition.z = ZPositionWithLayering();

            card.MoveToPosition(targetPosition);
        }

        public Stack<CardData> GetResetStack()
        {
            Stack<CardData> newStack = new Stack<CardData>();
            for (int i = PlayingCardsInStack.Count - 1; i >= 0; i--)
            {
                newStack.Push(PlayingCardsInStack[i].CardData);
            }

            ClearCards();
            return newStack;
        }

        void ClearCards()
        {
            foreach (PlayingCard pc in PlayingCardsInStack) Destroy(pc.gameObject);
            PlayingCardsInStack.Clear();
        }
    }
}