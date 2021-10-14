using System;
using System.Linq;

namespace Solitaire
{
    public class FoundationStack : Stack
    {
        public static event Action OnCardAddedToFoundation;

        public override bool CanAddCard(PlayingCard card)
        {
            if (PlayingCardsInStack.Count == 0) return card.CardData.Rank == 0;
            return IsSequentialSameSuit(card);
        }

        bool IsSequentialSameSuit(PlayingCard card)
        {
            return card.CardData.Suit == PlayingCardsInStack.Last().CardData.Suit &&
                   card.CardData.Rank == PlayingCardsInStack.Last().CardData.Rank + 1;
        }

        public int GetFoundationSuit() => PlayingCardsInStack[0].CardData.Suit;
        public bool IsComplete() => PlayingCardsInStack.Count == 13;
        

        public override void AddCard(PlayingCard card)
        {
            base.AddCard(card);
            OnCardAddedToFoundation?.Invoke();
        }
    }
}