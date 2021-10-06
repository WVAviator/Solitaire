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
            foreach (PlayingCard card in PlayingCardsInStack) card.transform.position = transform.position;
        }

        protected override void SetPosition(PlayingCard card)
        {
            int positionInDraw = currentDrawSize - currentDrawAmount--;
            card.transform.position = transform.position - new Vector3(0, positionInDraw * CardSpacing, 0);
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