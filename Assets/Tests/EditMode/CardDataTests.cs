using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;
using UnityEngine;

public class CardDataTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        public void Color_CardDataSuit_ReturnsCorrectColor(int suit, int expected)
        {
            CardData card = new CardData(suit, 0);
            int actual = card.Color;
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(0, "Hearts")]
        [TestCase(1, "Diamonds")]
        [TestCase(2, "Clubs")]
        [TestCase(3, "Spades"]
        public void SuitName_CardDataSuit_ReturnsCorrectName(int suit, string expected)
        {
            CardData card = new CardData(suit, 0);
            string actual = card.SuitName;
            Assert.AreEqual(expected, actual);
        }
    
        [TestCase(0, "Ace")]
        [TestCase(1, "Two")]
        [TestCase(2, "Three")]
        [TestCase(3, "Four")]
        [TestCase(4, "Five")]
        [TestCase(5, "Six")]
        [TestCase(6, "Seven")]
        [TestCase(7, "Eight")]
        [TestCase(8, "Nine")]
        [TestCase(9, "Ten")]
        [TestCase(10, "Jack")]
        [TestCase(11, "Queen")]
        [TestCase(12, "King")]   
        public void RankName_CardDataRank_ReturnsCorrectName(int rank, string expected)
        {
            CardData card = new CardData(0, rank);
            string actual = card.RankName;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CardSprites_ExistInDirectory()
        {
            Deck deck = new Deck();
            for (int i = 0; i < 52; i++)
            {
                CardData card = deck.DrawCard();
                string path = $"Sprites/Cards/{card.SuitName}/{card.RankName}";
                Sprite s = Resources.Load<Sprite>(path);
                Assert.NotNull(s, $"Sprite for {card.RankName} of {card.SuitName} not found in directory: {path}.");
            }
        }
        
    }
