using System;
using System.Collections.Generic;

namespace Solitaire
{
    [Serializable]
    public struct CardData
    {
        public int Suit => suit;
        int suit;
        public int Rank => rank;
        int rank;
        public int Color => suit / 2;
        public int Index => suit * 13 + rank;
        public string Name => $"{rankNames[rank]} +  of  + {suitNames[suit]}";

        public CardData(int suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        static List<string> rankNames = new List<string>()
            {"Ace", "Deuce", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

        static List<string> suitNames = new List<string>()
            {"Hearts", "Diamonds", "Clubs", "Spades"};
    }
}