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
    public void DeckCardCount()
    {
        Deck deck = new Deck();
        Assert.AreEqual(52, deck.CardsRemaining());
    }

    [Test]
    public void DeckCardDraw()
    {
        Deck deck = new Deck();
        List<CardData> cards = new List<CardData>();
        cards.Add(deck.DrawCard());
        Assert.AreEqual(51, deck.CardsRemaining());
        Assert.AreEqual(1, cards.Count);

        for (int i = 0; i < 51; i++)
        {
            cards.Add(deck.DrawCard());
        }
        Assert.AreEqual(0, deck.CardsRemaining());
        Assert.AreEqual(52, cards.Count);
    }

    [Test]
    public void AllCardsUnique()
    {
        Deck deck = new Deck();
        List<CardData> cards = new List<CardData>();
        for (int i = 0; i < 52; i++)
        {
            cards.Add(deck.DrawCard());
        }

        int distinctCount = cards.Distinct().Count();
        Assert.AreEqual(52, distinctCount);

        List<CardData> allSpades = new List<CardData>(cards.Where(p => p.Suit == 3));
        Assert.AreEqual(13, allSpades.Count);
    }
    
    

}
