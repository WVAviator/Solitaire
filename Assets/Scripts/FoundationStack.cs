using System;
using System.Collections.Generic;
using System.Linq;

namespace Solitaire
{
    public class FoundationStack : Stack
    {
        public static event Action<List<FoundationStack>> OnAllFoundationsComplete;

        static readonly List<FoundationStack> Foundations = new List<FoundationStack>();

        public override bool CanAddCard(PlayingCard card) => 
            CardStack.Count == 0 ? IsAce(card) : IsSequentialSameSuit(card);

        public int GetFoundationSuit() => CardStack[0].CardInfo.Suit;

        protected override void OnEnable()
        {
            Foundations.Add(this);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            Foundations.Remove(this);
            base.OnDisable();
        }

        public override void AddCard(PlayingCard card)
        {
            base.AddCard(card);

            if (!AllFoundationsComplete()) return;

            OnAllFoundationsComplete?.Invoke(Foundations);
            Foundations.ForEach(f => f.Clear());
        }

        static bool IsAce(PlayingCard card) => card.CardInfo.Rank == 0;
        
        static bool AllFoundationsComplete() => Foundations.All(f => f.IsComplete());

        bool IsSequentialSameSuit(PlayingCard card)
        {
            return card.CardInfo.Suit == CardStack.Last().CardInfo.Suit &&
                   card.CardInfo.Rank == CardStack.Last().CardInfo.Rank + 1;
        }

        bool IsComplete() => CardStack.Count == 13;
    }
}