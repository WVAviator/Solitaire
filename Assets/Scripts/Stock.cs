﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class Stock : MonoBehaviour, IClickable
    {
        public Deck Deck { get; private set; }
        int _stockPasses = int.MaxValue;
        public int DrawCount { get; private set; } = 3;

        public int StockPassesRemaining { get; set; }

        [SerializeField] PlayingCard cardPrefab;
        [SerializeField] float cardDealSpeed = 0.05f;

        WasteStack _wasteStack;
        bool _dealInProgress;
        public static event Action OnNewGameDeal;
        public event Action OnDeckChanged;

        void Awake()
        {
            Deck = new Deck();
            _wasteStack = FindObjectOfType<WasteStack>();
            
        }

        void OnEnable() => Settings.OnSettingsUpdated += UpdateSettings;
        void OnDisable() => Settings.OnSettingsUpdated -= UpdateSettings;

        void Start() => DealNewGame();

        void UpdateSettings(Settings settings)
        {
            DrawCount = settings.DrawCount;
            _stockPasses = settings.StockPasses;
        }

        public void DealNewGame()
        {
            if (_dealInProgress) return;
            Deck = new Deck();
            
            StockPassesRemaining = _stockPasses;

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
                    PlayingCard card = DrawAndSetNewPlayingCard(i == j);
                    Stack tableauStack = TableauStack.Tableaux[j];
                    tableauStack.AddCard(card);
                    card.UpdateCurrentStack(tableauStack);
                    
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }

            _dealInProgress = false;
        }

        void DealCardsToWaste(int cardsToDraw)
        {
            PlayingCard[] cards = GetCards(cardsToDraw);
            WasteDeal wasteDeal = new WasteDeal(this, _wasteStack, cards);
            wasteDeal.Process();
        }

        public void RestoreWaste()
        {
            int cardsToDraw = Deck.CardsRemaining();
            PlayingCard[] cards = GetCards(cardsToDraw);
            _wasteStack.PlayingCardsInStack = new List<PlayingCard>(cards);
            OnDeckChanged?.Invoke();
        }

        PlayingCard[] GetCards(int cardsToGet)
        {
            PlayingCard[] cards = new PlayingCard[cardsToGet];
            for (int i = 0; i < cardsToGet; i++)
            {
                cards[i] = DrawAndSetNewPlayingCard(true);
            }

            return cards;
        }

        public void SetDeck(Deck deck)
        {
            Deck = deck;
            OnDeckChanged?.Invoke();
        }

        int GetNumberOfCardsToDraw()
        {
            int drawAmount = DrawCount;
            if (Deck.CardsRemaining() < DrawCount) drawAmount = Deck.CardsRemaining();
            return drawAmount;
        }

        void RecycleWaste()
        {
            if (StockPassesRemaining <= 0) return;
            
            WasteRecycle wasteRecycle = new WasteRecycle(this, _wasteStack);
            wasteRecycle.Process();
        }

        PlayingCard DrawAndSetNewPlayingCard(bool turnFaceUp)
        {
            PlayingCard newCard =
                Instantiate(cardPrefab, transform.position, Quaternion.identity);
            newCard.SetCard(DrawCard());
            if (turnFaceUp) newCard.FlipNoUndo();
            return newCard;
        }

        CardInfo DrawCard()
        {
            CardInfo nextCard = Deck.DrawCard();
            OnDeckChanged?.Invoke();
            return nextCard;
        }

        public void ReturnToDeck(PlayingCard card)
        {
            Deck.AddToStack(card.CardInfo);
            Destroy(card.gameObject);
            OnDeckChanged?.Invoke();
        }
    }
}