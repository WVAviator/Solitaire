using System.Collections.Generic;

namespace Solitaire
{
    public class Deck
    {
        readonly Stack<CardData> cardStack;
        public Deck()
        {
            List<CardData> cards = new List<CardData>();
            for (int r = 0; r < 13; r++)
            {
                for (int s = 0; s < 4; s++)
                {
                    CardData newCard = new CardData(s, r);
                    cards.Add(newCard);
                }
            }
            
            cards.Shuffle();
            cardStack = new Stack<CardData>(cards);
        }

        public Deck(Stack<CardData> cardStack)
        {
            this.cardStack = cardStack;
        }

        public CardData DrawCard() => cardStack.Pop();
        
        public int CardsRemaining() => cardStack.Count;
        
    }
}