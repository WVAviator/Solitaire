using System;
using System.Collections.Generic;

namespace Solitaire
{
    [Serializable]
    public struct CardData
    {
        public int Suit => _suit;
        int _suit;
        public int Rank => _rank;
        int _rank;
        public int Color => _suit / 2;
        public int Index => _suit * 13 + _rank;
        public string Name => $"{rankNames[_rank]} +  of  + {suitNames[_suit]}";

        public CardData(int suit, int rank)
        {
            this._suit = suit;
            this._rank = rank;
        }

        static List<string> rankNames = new List<string>()
            {"Ace", "Deuce", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

        static List<string> suitNames = new List<string>()
            {"Hearts", "Diamonds", "Clubs", "Spades"};
    }
}