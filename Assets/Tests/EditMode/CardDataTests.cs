using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;

public class CardDataTests
    {

        [Test]
        public void CardIndexTest()
        {
            CardData aceOfHearts = new CardData(0, 0);
            Assert.AreEqual(0, aceOfHearts.Index);

            CardData fourOfClubs = new CardData(2, 3);
            Assert.AreEqual(29, fourOfClubs.Index);

            CardData kingOfSpades = new CardData(3, 12);
            Assert.AreEqual(51, kingOfSpades.Index);
        }

        [Test]
        public void CardColorValue()
        {
            CardData aceOfDiamonds = new CardData(1, 0);
            CardData aceOfClubs = new CardData(2, 0);
            Assert.AreEqual(false, aceOfClubs.Color == aceOfDiamonds.Color);
        }
        
    }
