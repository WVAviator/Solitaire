using System;
using System.Collections;
using UnityEngine;

namespace Solitaire
{
    public class Stock : MonoBehaviour, IClickable
    {
        public Deck Deck { get; private set; }

        public int DrawCount => drawCount;
        [SerializeField] int drawCount = 3;
        
        [SerializeField] PlayingCard cardPrefab;
        [SerializeField] float cardDealSpeed = 0.05f;
        
        WasteStack _wasteStack;
        StockSounds _stockSounds;
        bool _dealInProgress;
        public static event Action OnNewGameDeal;
        public event Action OnDeckChanged;
        void Awake()
        {
            Deck = new Deck();
            _wasteStack = FindObjectOfType<WasteStack>();
            _stockSounds = GetComponent<StockSounds>();
        }
        void Start() => DealNewGame();
        public void Click()
        {
            int numberOfCardsToDraw = GetNumberOfCardsToDraw();

            if (numberOfCardsToDraw == 0)
            {
                RecycleWaste();
                return;
            }

            PlayingCard[] cards = DealCardsToWaste(numberOfCardsToDraw);
            _wasteStack.RevealCards(cards);
        }

        PlayingCard[] DealCardsToWaste(int cardsToDraw)
        {
            PlayingCard[] cards = new PlayingCard[cardsToDraw];
            for (int i = 0; i < cardsToDraw; i++)
            {
                cards[i] = DrawAndSetNewPlayingCard(true);
            }
            return cards;
        }

        int GetNumberOfCardsToDraw()
        {
            int drawAmount = drawCount;
            if (Deck.CardsRemaining() < drawCount) drawAmount = Deck.CardsRemaining();
            return drawAmount;
        }

        void RecycleWaste()
        {
            Deck = new Deck(_wasteStack.GetRecycledStock());
            OnDeckChanged?.Invoke();
        }
        
        public void DealNewGame()
        {
            if (_dealInProgress) return;
            Deck = new Deck();
            
            OnNewGameDeal?.Invoke();
            _stockSounds.PlayDealSound();

            StartCoroutine(DealCardsToTableau());
        }
        IEnumerator DealCardsToTableau()
        {
            _dealInProgress = true;

            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    TableauStack.Tableaux[j].AddCard(DrawAndSetNewPlayingCard(i == j));
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }

            _dealInProgress = false;
        }

        PlayingCard DrawAndSetNewPlayingCard(bool turnFaceUp)
        {
            PlayingCard newCard =
                Instantiate(cardPrefab, transform.position, Quaternion.identity);
            newCard.SetCard(DrawCard());
            if (turnFaceUp) newCard.Click();
            return newCard;
        }
        CardInfo DrawCard()
        {
            CardInfo nextCard = Deck.DrawCard();
            OnDeckChanged?.Invoke();
            return nextCard;
        }
    }
}