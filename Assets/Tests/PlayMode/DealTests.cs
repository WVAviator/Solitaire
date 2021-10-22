using System.Collections;
using System.Linq;
using NUnit.Framework;
using Solitaire;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class DealTests
    {

        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Solitaire");
        }
    
        [UnityTest]
        public IEnumerator DealNewGame_DealCards_CorrectCardsRemainInStock()
        {
            yield return new WaitForSeconds(2);

            Stock stock = Object.FindObjectOfType<Stock>();
            Assert.AreEqual(24, stock.Deck.CardsRemaining());
        }

        [UnityTest]
        public IEnumerator GameBoard_Setup_CorrectNumberOfFoundationStacks()
        {
            yield return new WaitForSeconds(1);
            FoundationStack[] stacks = Object.FindObjectsOfType<FoundationStack>();
            Assert.AreEqual(4, stacks.Length);
        }
    
        [UnityTest]
        public IEnumerator GameBoard_Setup_CorrectNumberOfTableauStacks()
        {
            yield return new WaitForSeconds(1);
            TableauStack[] stacks = Object.FindObjectsOfType<TableauStack>();
            Assert.AreEqual(7, stacks.Length);
        }
    
        [UnityTest]
        public IEnumerator GameBoard_Setup_CorrectNumberOfWasteStacks()
        {
            yield return new WaitForSeconds(1);
            WasteStack[] stacks = Object.FindObjectsOfType<WasteStack>();
            Assert.AreEqual(1, stacks.Length);
        }
    
        [UnityTest]
        public IEnumerator DealNewGame_DealCards_CorrectCardsDealtToTableau()
        {
            yield return new WaitForSeconds(2);
            TableauStack[] stacks = Object.FindObjectsOfType<TableauStack>();
            PlayingCard[] cards = Object.FindObjectsOfType<PlayingCard>();
            stacks = stacks.OrderBy(s => s.transform.position.x).ToArray();
            for (int i = 0; i < stacks.Length; i++)
            {
                PlayingCard card = GetCardAtPosition(stacks[i].transform.position, cards);
                int expected = i + 1;
                int actual = card.GetComponentsInChildren<PlayingCard>().Length;
                Assert.AreEqual(expected, actual, $"Incorrect card count in stack {stacks[i]}: Expected: {expected} but found {actual}");
            }
        }

        PlayingCard GetCardAtPosition(Vector2 position, PlayingCard[] cards)
        {
            PlayingCard closest = cards[0];
            foreach (PlayingCard card in cards)
            {
                if (((Vector2)card.transform.position - position).magnitude < ((Vector2)closest.transform.position - position).magnitude)
                    closest = card;
            }

            return closest;
        }
    }
}
