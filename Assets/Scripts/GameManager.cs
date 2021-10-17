using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class GameManager : MonoBehaviour
    {
        Stock stock;

        public static event Action OnNewGameDeal;

        bool _dealInProgress;

        [SerializeField] Stock stockPrefab;
        [SerializeField] PlayingCard cardPrefab;

        [SerializeField] float cardDealSpeed = 0.05f;

        void Start() => DealNewGame();
        public void DealNewGame()
        {
            if (_dealInProgress) return;
            
            OnNewGameDeal?.Invoke();

            CreateNewPlayingDeck();
            
            StartCoroutine(DealMainStacks());
        }

        void CreateNewPlayingDeck()
        {
            Vector2 stockLocation = GameBoard.Instance.StockLocation;
            stock = Instantiate(stockPrefab, stockLocation, Quaternion.identity);
        }

        IEnumerator DealMainStacks()
        {
            _dealInProgress = true;
            
            List<TableauStack> tableauStacks = GameBoard.Instance.Tableaux;
            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    PlayingCard newCard =
                        Instantiate(cardPrefab, GameBoard.Instance.StockLocation, Quaternion.identity);
                    newCard.SetCard(stock.DrawCard());
                    tableauStacks[j].AddCard(newCard);
                    if (i == j) newCard.TurnFaceUp();
                    yield return new WaitForSeconds(cardDealSpeed);
                }
            }

            _dealInProgress = false;
        }

        public void QuitGame()
        {
            #if UNITY_WEBGL
                return;
            #endif
            
            Application.Quit();
        }

    }
}