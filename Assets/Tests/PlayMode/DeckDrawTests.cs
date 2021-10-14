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

        Stock deck = Object.FindObjectOfType<Stock>();
        WasteStack wasteStack = Object.FindObjectOfType<WasteStack>();
        
        Assert.AreEqual(24, deck.Deck.CardsRemaining());
        
        deck.Click();
        Assert.AreEqual(24 - deck.DrawCount, deck.Deck.CardsRemaining());
        Assert.AreEqual(deck.DrawCount, wasteStack.CardsInStack());

        while (deck.Deck.CardsRemaining() > 0) deck.Click();
        Assert.AreEqual(0, deck.Deck.CardsRemaining());
        Assert.AreEqual(24, wasteStack.CardsInStack());
        
        deck.Click();
        Assert.AreEqual(24, deck.Deck.CardsRemaining());
        Assert.AreEqual(0, wasteStack.CardsInStack());

        yield return null;
    }

    [UnityTest]
    public IEnumerator SetupTest()
    {
        yield return new WaitForSeconds(5);
        
        List<TableauStack> mainStacks = new List<TableauStack>(Object.FindObjectsOfType<TableauStack>().OrderBy(m => m.gameObject.name));
        int totalCardsDealt = 0;

        for (int i = 0; i < 7; i++)
        {
            totalCardsDealt += mainStacks[i].CardsInStack();
            Assert.AreEqual(i + 1, mainStacks[i].CardsInStack());
        }
        
        Assert.AreEqual(28, totalCardsDealt);
        
    }
}
