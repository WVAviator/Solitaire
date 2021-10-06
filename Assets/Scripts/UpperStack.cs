using System;
using System.Linq;

namespace Solitaire
{
    public class UpperStack : Stack
    {
        public static event Action OnCardAddedToUpperStack = delegate { };

        public override bool CanAddCard(PlayingCard card)
        {
            if (PlayingCardsInStack.Count == 0) return card.card.GetRank() == 0;
            return IsSequentialSameSuit(card);
        }

        bool IsSequentialSameSuit(PlayingCard card)
        {
            return card.card.GetSuit() == PlayingCardsInStack.Last().card.GetSuit() &&
                   card.card.GetRank() == PlayingCardsInStack.Last().card.GetRank() + 1;
        }

        public int GetActiveSuit()
        {
            return PlayingCardsInStack[0].card.GetSuit();
        }

        public bool IsComplete()
        {
            return PlayingCardsInStack.Count == 13 && PlayingCardsInStack[12].card.GetRank() == 12;
        }

        public override void AddCard(PlayingCard card)
        {
            base.AddCard(card);
            OnCardAddedToUpperStack?.Invoke();
        }
    }
}