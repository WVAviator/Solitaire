using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class GameManager : MonoBehaviour
    {
        PlayingDeck activeDeck;

        public static event Action OnGameWin;
        public static event Action OnNewGameDealing;
        public static event Action OnNewGameDealt;
        
        UpperStack[] upperStacks;

        [SerializeField] PlayingDeck deckPrefab;
        [SerializeField] PlayingCard cardPrefab;

        [SerializeField] float cardDealSpeed = 0.05f;

        void Awake()
        {
            UpperStack.OnCardAddedToUpperStack += CheckForWin;
            upperStacks = FindObjectsOfType<UpperStack>();
        }

        void CheckForWin()
        {
            if (!upperStacks.All(us => us.IsComplete())) return;
            ClearTable();
            OnGameWin?.Invoke();
            StartCoroutine(DelayStartNewGame());
        }

        IEnumerator DelayStartNewGame()
        {
            yield return new WaitForSeconds(4);
            DealNewGame();
        }

        void Start() => DealNewGame();


        public void DealNewGame()
        {
            ClearTable();
            CreateNewPlayingDeck();
            OnNewGameDealing?.Invoke();
            StartCoroutine(DealMainStacks());
        }
        

        void ClearTable()
        {
            DestroyCards();
            DestroyDeck();
            ClearStackLists();
        }

        static void ClearStackLists()
        {
            Stack[] stacks = FindObjectsOfType<Stack>();
            foreach (Stack s in stacks) s.Clear();
        }

        void DestroyDeck()
        {
            if (activeDeck != null) Destroy(activeDeck.gameObject);
        }

        static void DestroyCards()
        {
            PlayingCard[] cards = FindObjectsOfType<PlayingCard>();
            foreach (PlayingCard card in cards) Destroy(card.gameObject);
        }


        void CreateNewPlayingDeck()
        {
            Vector2 deckLocation = GameBoard.Instance.DeckLocation;
            activeDeck = Instantiate(deckPrefab, deckLocation, Quaternion.identity);
        }

        IEnumerator DealMainStacks()
        {
            MainStack[] mainStacks = GameBoard.Instance.MainStacks.ToArray();
            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    PlayingCard newCard =
                        Instantiate(cardPrefab, GameBoard.Instance.DeckLocation, Quaternion.identity);
                    newCard.SetCard(activeDeck.DrawCard());
                    mainStacks[j].AddCard(newCard);
                    if (i == j) newCard.TurnFaceUp();
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }
            OnNewGameDealt?.Invoke();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}