using System;
using System.Collections.Generic;
using System.Linq;

namespace Solitaire
{
    public class FoundationStack : Stack
    {
        public static event Action<List<FoundationStack>> OnAllFoundationsComplete;
        static readonly List<FoundationStack> Foundations = new List<FoundationStack>();

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

        public override bool CanAddCard(PlayingCard card)
        {
            if (PlayingCardsInStack.Count == 0) return card.CardInfo.Rank == 0;
            return IsSequentialSameSuit(card);
        }

        bool IsSequentialSameSuit(PlayingCard card)
        {
            return card.CardInfo.Suit == PlayingCardsInStack.Last().CardInfo.Suit &&
                   card.CardInfo.Rank == PlayingCardsInStack.Last().CardInfo.Rank + 1;
        }

        public int GetFoundationSuit() => PlayingCardsInStack[0].CardInfo.Suit;
        public bool IsComplete() => PlayingCardsInStack.Count == 13;
        

        public override void AddCard(PlayingCard card)
        {
            base.AddCard(card);
            
            if (!AllFoundationsComplete()) return;
            
            OnAllFoundationsComplete?.Invoke(Foundations);
            Foundations.ForEach(f => f.Clear());
        }
        
        static bool AllFoundationsComplete() => Foundations.All(f => f.IsComplete());
    }
}