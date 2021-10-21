using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;
using UnityEngine;

public class CardDataTests
    {
        [Test]
        public void CardColorValue()
        {
            CardData aceOfDiamonds = new CardData(1, 0);
            CardData aceOfClubs = new CardData(2, 0);
            Assert.AreEqual(false, aceOfClubs.Color == aceOfDiamonds.Color);
        }

        [Test]
        public void CardNameMatchesInstancedValue()
        {
            for (int i = 0; i < 13; i++)
            {
                Assert.AreEqual((new CardData(0, i)).SuitName, "Hearts");
                Assert.AreEqual((new CardData(1, i)).SuitName, "Diamonds");
                Assert.AreEqual((new CardData(2, i)).SuitName, "Clubs");
                Assert.AreEqual((new CardData(3, i)).SuitName, "Spades");
            }

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual((new CardData(i, 0)).RankName, "Ace");
                Assert.AreEqual((new CardData(i, 1)).RankName, "Two");
                Assert.AreEqual((new CardData(i, 2)).RankName, "Three");
                Assert.AreEqual((new CardData(i, 3)).RankName, "Four");
                Assert.AreEqual((new CardData(i, 4)).RankName, "Five");
                Assert.AreEqual((new CardData(i, 5)).RankName, "Six");
                Assert.AreEqual((new CardData(i, 6)).RankName, "Seven");
                Assert.AreEqual((new CardData(i, 7)).RankName, "Eight");
                Assert.AreEqual((new CardData(i, 8)).RankName, "Nine");
                Assert.AreEqual((new CardData(i, 9)).RankName, "Ten");
                Assert.AreEqual((new CardData(i, 10)).RankName, "Jack");
                Assert.AreEqual((new CardData(i, 11)).RankName, "Queen");
                Assert.AreEqual((new CardData(i, 12)).RankName, "King");
            }
        }

        [Test]
        public void CardSpritesExistInFolder()
        {
            Deck deck = new Deck();
            for (int i = 0; i < 52; i++)
            {
                CardData card = deck.DrawCard();
                string path = $"Sprites/Cards/{card.SuitName}/{card.RankName}";
                Sprite s = Resources.Load<Sprite>(path);
                Assert.NotNull(s, $"Sprite for {card.RankName} of {card.SuitName} not found.");
            }
        }
        
    }
