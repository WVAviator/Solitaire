using System;
using System.Collections.Generic;

namespace Solitaire
{
    [Serializable]
    public struct Card
    {
        int suit;
        int rank;

        public Card(int suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        static List<string> rankNames = new List<string>()
            {"Ace", "Deuce", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

        static List<string> suitNames = new List<string>()
            {"Hearts", "Diamonds", "Clubs", "Spades"};

        public string CardName()
        {
            return rankNames[rank] + " of " + suitNames[suit];
        }

        public int GetSuit()
        {
            return suit;
        }

        public int GetRank()
        {
            return rank;
        }

        public int GetCardIndex()
        {
            return suit * 13 + rank;
        }

        public int GetColor()
        {
            if (suit == 0 || suit == 1) return 0;
            return 1;
        }
    }
}