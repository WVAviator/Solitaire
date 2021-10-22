using UnityEngine;

namespace Solitaire
{
    public class StockVisuals : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Stock stock;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            stock = GetComponent<Stock>();
        }

        void OnEnable() => stock.OnDeckChanged += UpdateStockSprite;
        void OnDisable() => stock.OnDeckChanged -= UpdateStockSprite;
        
        void UpdateStockSprite() => _spriteRenderer.enabled = stock.Deck.CardsRemaining() != 0;


    }
}