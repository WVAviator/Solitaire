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

        public void DealNewGame()
        {
            if (_dealInProgress) return;
            Deck = new Deck();

            OnNewGameDeal?.Invoke();

            StartCoroutine(DealCardsToTableau());
        }

        public void Click()
        {
            int numberOfCardsToDraw = GetNumberOfCardsToDraw();

            if (numberOfCardsToDraw == 0) RecycleWaste();
            else DealCardsToWaste(numberOfCardsToDraw);
        }

        IEnumerator DealCardsToTableau()
        {
            _dealInProgress = true;

            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    StackTransferRequest stackTransferRequest =
                        new StackTransferRequest(DrawAndSetNewPlayingCard(i == j), TableauStack.Tableaux[j], true);
                    stackTransferRequest.Process();
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }

            _dealInProgress = false;
        }

        void DealCardsToWaste(int cardsToDraw)
        {
            _wasteStack.ResetRevealedCards();

            PlayingCard[] cards = new PlayingCard[cardsToDraw];
            for (int i = 0; i < cardsToDraw; i++)
            {
                cards[i] = DrawAndSetNewPlayingCard(true);
                _wasteStack.AddToRevealedCards(cards[i]);

                StackTransferRequest stackTransferRequest = new StackTransferRequest(cards[i], _wasteStack, true);
                stackTransferRequest.Process();
            }
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