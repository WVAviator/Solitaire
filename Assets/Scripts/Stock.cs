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
        SpriteRenderer _spriteRenderer;
        StockSounds _stockSounds;
        bool _dealInProgress;
        public static event Action OnNewGameDeal;
        void Awake()
        {
            Deck = new Deck();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _wasteStack = FindObjectOfType<WasteStack>();
            _stockSounds = GetComponent<StockSounds>();
        }
        void Start() => DealNewGame();

        public CardInfo DrawCard()
        {
            CardInfo nextCard = Deck.DrawCard();
            UpdateStockSprite();
            return nextCard;
        }

        void UpdateStockSprite() => _spriteRenderer.enabled = Deck.CardsRemaining() != 0;
        

        public void Click()
        {
            int numberOfCardsToDraw = GetNumberOfCardsToDraw();

            if (numberOfCardsToDraw == 0)
            {
                RecycleWaste();
                return;
            }

            PlayingCard[] cards = DrawCardsFromDeck(numberOfCardsToDraw);
            _wasteStack.RevealCards(cards);
        }

        PlayingCard[] DrawCardsFromDeck(int cardsToDraw)
        {
            PlayingCard[] cards = new PlayingCard[cardsToDraw];
            for (int i = 0; i < cardsToDraw; i++)
            {
                cards[i] = DrawAndSetNewCard(true);
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
            UpdateStockSprite();
        }
        
        public void DealNewGame()
        {
            if (_dealInProgress) return;
            Deck = new Deck();
            
            OnNewGameDeal?.Invoke();
            _stockSounds.PlayDealSound();

            StartCoroutine(DealMainStacks());
        }
        IEnumerator DealMainStacks()
        {
            _dealInProgress = true;

            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    TableauStack.Tableaux[j].AddCard(DrawAndSetNewCard(i == j));
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }

            _dealInProgress = false;
        }

        PlayingCard DrawAndSetNewCard(bool turnFaceUp)
        {
            PlayingCard newCard =
                Instantiate(cardPrefab, transform.position, Quaternion.identity);
            newCard.SetCard(DrawCard());
            if (turnFaceUp) newCard.TurnFaceUp();
            return newCard;
        }
    }
}