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

        yield return null;
    }
}
