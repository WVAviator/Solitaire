using System;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class GameManager : MonoBehaviour
    {
        PlayingDeck activeDeck;

        public static event Action OnGameWin;
        public static event Action OnNewGameDealt;
        UpperStack[] upperStacks;

        [SerializeField] PlayingDeck deckPrefab;
        [SerializeField] PlayingCard cardPrefab;

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
        }

        void Start() => DealNewGame();


        public void DealNewGame()
        {
            ClearTable();
            CreateNewPlayingDeck();
            DealMainStacks();
            OnNewGameDealt?.Invoke();
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

        void DealMainStacks()
        {
            MainStack[] mainStacks = GameBoard.Instance.MainStacks.ToArray();
            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    PlayingCard newCard =
                        Instantiate(cardPrefab, mainStacks[j].transform.position, Quaternion.identity);
                    newCard.SetCard(activeDeck.DrawCard());
                    mainStacks[j].AddCard(newCard);
                    if (i == j) newCard.TurnFaceUp();
                }
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}