using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Solitaire;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class WinEffectsTests
    {

        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Solitaire");
        }
        
        [UnityTest]
        public IEnumerator InitiateEffects_WinEffects_LaunchAfterFillingFoundationStacks()
        {
            yield return new WaitForSeconds(2);
            
            Stock stock = Object.FindObjectOfType<Stock>();
            FoundationStack[] stacks = Object.FindObjectsOfType<FoundationStack>();
            
            for (int i = 0; i < 24 / stock.DrawCount; i++)
            {
                stock.Click();
            }

            PlayingCard[] cards = Object.FindObjectsOfType<PlayingCard>();
            foreach (PlayingCard card in cards)
            {
                stacks[card.CardInfo.Suit].Transfer(card, null);
            }

            yield return new WaitForSeconds(2);
            
            PlayingCard[] cardsLeft = Object.FindObjectsOfType<PlayingCard>();
            
            Assert.AreEqual(0, cardsLeft.Length);
        }
        
    }
}