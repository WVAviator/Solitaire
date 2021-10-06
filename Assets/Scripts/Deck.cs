using System.Collections.Generic;

namespace Solitaire
{
    public class Deck
    {
        readonly List<Card> cards = new List<Card>();
        readonly Stack<Card> stack;

        public Deck()
        {
            for (int r = 0; r < 13; r++)
            {
                for (int s = 0; s < 4; s++)
                {
                    Card newCard = new Card(s, r);
                    cards.Add(newCard);
                }
            }

            cards.Shuffle();
            stack = new Stack<Card>(cards);
        }

        public Deck(Stack<Card> cards)
        {
            stack = cards;
        }

        public Card DrawCard()
        {
            return stack.Pop();
        }

        public int CardsRemaining()
        {
            return stack.Count;
        }
    }
}