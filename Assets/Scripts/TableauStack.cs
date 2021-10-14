using System.Collections.Generic;
using System.Linq;

namespace Solitaire
{
    public class TableauStack : Stack
    {
        public override void AddCard(PlayingCard card)
        {
            List<PlayingCard> allCards = new List<PlayingCard>( card.GetComponentsInChildren<PlayingCard>());
            foreach (PlayingCard c in allCards) base.AddCard(c);
        }
        public override bool CanAddCard(PlayingCard card)
        {
            int rank = card.CardData.Rank;
            int color = card.CardData.Color;

            if (EmptyStack()) return IsKing(card);

            PlayingCard lastCard = PlayingCardsInStack.Last();

            return IsSequentialAlternatingColor(rank, color, lastCard);
        }
        static bool IsKing(PlayingCard card) => card.CardData.Rank == 12;
        bool EmptyStack() => PlayingCardsInStack.Count == 0;
        bool IsSequentialAlternatingColor(int rank, int color, PlayingCard lastCard)
        {
            return rank == lastCard.CardData.Rank - 1 && color != lastCard.CardData.Color;
        }
    }
}