using System.Collections.Generic;
using System.Linq;

namespace Solitaire
{
    public class TableauStack : Stack
    {
        public static List<TableauStack> Tableaux = new List<TableauStack>();

        protected override void OnEnable()
        {
            Tableaux.Add(this);
            Tableaux = Tableaux.OrderBy(t => t.transform.position.x).ToList();
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            Tableaux.Remove(this);
            base.OnDisable();
        }

        public override bool CanAddCard(PlayingCard card)
        {
            int rank = card.CardInfo.Rank;
            int color = card.CardInfo.Color;

            if (EmptyStack()) return IsKing(card);

            PlayingCard lastCard = PlayingCardsInStack.Last();

            return IsSequentialAlternatingColor(rank, color, lastCard);
        }

        public override void AddCard(PlayingCard card)
        {
            List<PlayingCard> allCards = new List<PlayingCard>( card.GetComponentsInChildren<PlayingCard>());
            allCards.OrderByDescending(c => c.CardInfo.Rank);

            foreach (PlayingCard c in allCards)
            {
                base.AddCard(c);
                c.UpdateCurrentStack(this);
            }
        }

        static bool IsKing(PlayingCard card) => card.CardInfo.Rank == 12;
        bool EmptyStack() => PlayingCardsInStack.Count == 0;
        bool IsSequentialAlternatingColor(int rank, int color, PlayingCard lastCard)
        {
            return rank == lastCard.CardInfo.Rank - 1 && color != lastCard.CardInfo.Color;
        }
    }
}