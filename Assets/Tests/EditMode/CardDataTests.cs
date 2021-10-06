using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;

public class CardDataTests
    {

        [Test]
        public void CardIndexTest()
        {
            Card aceOfHearts = new Card(0, 0);
            Assert.AreEqual(0, aceOfHearts.GetCardIndex());

            Card fourOfClubs = new Card(2, 3);
            Assert.AreEqual(29, fourOfClubs.GetCardIndex());

            Card kingOfSpades = new Card(3, 12);
            Assert.AreEqual(51, kingOfSpades.GetCardIndex());
        }

        [Test]
        public void CardColorValue()
        {
            Card aceOfDiamonds = new Card(1, 0);
            Card aceOfClubs = new Card(2, 0);
            Assert.AreEqual(false, aceOfClubs.GetColor() == aceOfDiamonds.GetColor());
        }
        
    }
