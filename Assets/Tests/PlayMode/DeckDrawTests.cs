using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solitaire;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DeckDrawTests
{

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Solitaire");
    }
    
    [UnityTest]
    public IEnumerator DrawCardsTest()
    {
        yield return new WaitForSeconds(5);

        PlayingDeck deck = Object.FindObjectOfType<PlayingDeck>();
        DrawStack drawStack = Object.FindObjectOfType<DrawStack>();
        
        Assert.AreEqual(24, deck.Deck.CardsRemaining());
        
        deck.Click();
        Assert.AreEqual(24 - deck.drawCount, deck.Deck.CardsRemaining());
        Assert.AreEqual(deck.drawCount, drawStack.CardsInStack());

        while (deck.Deck.CardsRemaining() > 0) deck.Click();
        Assert.AreEqual(0, deck.Deck.CardsRemaining());
        Assert.AreEqual(24, drawStack.CardsInStack());
        
        deck.Click();
        Assert.AreEqual(24, deck.Deck.CardsRemaining());
        Assert.AreEqual(0, drawStack.CardsInStack());

        yield return null;
    }

    [UnityTest]
    public IEnumerator SetupTest()
    {
        yield return new WaitForSeconds(5);
        
        List<MainStack> mainStacks = new List<MainStack>(Object.FindObjectsOfType<MainStack>().OrderBy(m => m.gameObject.name));
        int totalCardsDealt = 0;

        for (int i = 0; i < 7; i++)
        {
            totalCardsDealt += mainStacks[i].CardsInStack();
            Assert.AreEqual(i + 1, mainStacks[i].CardsInStack());
        }
        
        Assert.AreEqual(28, totalCardsDealt);
        
    }
}
