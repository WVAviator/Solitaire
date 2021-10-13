using System.Collections.Generic;
using System.Linq;

namespace Solitaire
{
    public class Tableau : Stack
    {
        public override void AddCard(PlayingCard card)
        {
            List<PlayingCard> allCards = new List<PlayingCard>( card.GetComponentsInChildren<PlayingCard>());

            foreach (PlayingCard c in allCards)
            {
                base.AddCard(c);
            }
        }

        public override bool CanAddCard(PlayingCard card)
        {
            int rank = card.card.GetRank();
            int color = card.card.GetColor();

            if (EmptyStack()) return card.card.GetRank() == 12;

            PlayingCard lastCard = PlayingCardsInStack.Last();

            return IsSequentialAlternatingColor(rank, color, lastCard);
        }

        bool EmptyStack()
        {
            return PlayingCardsInStack.Count == 0;
        }

        bool IsSequentialAlternatingColor(int rank, int color, PlayingCard lastCard)
        {
            return rank == lastCard.card.GetRank() - 1 && color != lastCard.card.GetColor();
        }
    }
}