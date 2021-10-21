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
        public string RankName => rankNames[_rank];
        public string SuitName => suitNames[_suit];

        public CardData(int suit, int rank)
        {
            _suit = suit;
            _rank = rank;
        }

        static List<string> rankNames = new List<string>()
            {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};

        static List<string> suitNames = new List<string>()
            {"Hearts", "Diamonds", "Clubs", "Spades"};
    }
}