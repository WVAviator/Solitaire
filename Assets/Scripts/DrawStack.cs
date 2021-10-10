using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class DrawStack : Stack
    {
        int currentDrawAmount;
        int currentDrawSize;

        public void RevealCards(PlayingCard[] cards)
        {
            currentDrawAmount = cards.Length;
            currentDrawSize = currentDrawAmount;

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
                newPosition.z = -0.01f * PlayingCardsInStack.IndexOf(card);
                
                card.SetTargetPosition(newPosition);
            }
        }

        protected override void SetPosition(PlayingCard card)
        {
            int positionInDraw = currentDrawSize - currentDrawAmount--;
            
            Vector3 targetPosition;
            targetPosition.x = transform.position.x;
            targetPosition.y = transform.position.y - positionInDraw * CardSpacing;
            targetPosition.z = -0.01f * PlayingCardsInStack.Count;

            card.SetTargetPosition(targetPosition);
        }

        public Stack<Card> GetResetStack()
        {
            Stack<Card> newStack = new Stack<Card>();
            for (int i = PlayingCardsInStack.Count - 1; i >= 0; i--)
            {
                newStack.Push(PlayingCardsInStack[i].card);
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