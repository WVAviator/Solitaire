using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;
using UnityEngine;
using UnityEngine.TestTools;

public class DeckDataTests
{
    [Test]
    public void CardsRemaining_NewDeck_Has52Cards()
    {
        Deck deck = new Deck();
        Assert.AreEqual(52, deck.CardsRemaining());
    }
    
    [TestCase(1, 51)]
    [TestCase(2, 50)]
    [TestCase(5, 47)]
    [TestCase(52, 0)]
    [TestCase(53, 0)]
    [TestCase(0, 52)]
    public void DrawCard_CardsRemaining_ReducedBy(int amount, int expected)
    {
        Deck deck = new Deck();
        for (int i = 0; i < amount; i++) 
        {
            deck.DrawCard();
        }
        Assert.AreEqual(expected, deck.CardsRemaining());
    }
    
    [Test]
    public void DrawCard_AllCards_AreUnique()
    {
        Deck deck = new Deck();
        List<CardData> cards = new List<CardData>();
        for (int i = 0; i < 52; i++)
        {
            cards.Add(deck.DrawCard());
        }

        int distinctCount = cards.Distinct().Count();
        Assert.AreEqual(52, distinctCount);
    }
}
