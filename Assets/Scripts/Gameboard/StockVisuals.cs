using UnityEngine;

namespace Solitaire
{
    public class StockVisuals : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Stock _stock;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _stock = GetComponent<Stock>();
        }

        void OnEnable() => _stock.OnDeckChanged += UpdateStockSprite;
        void OnDisable() => _stock.OnDeckChanged -= UpdateStockSprite;
        
        void UpdateStockSprite() => _spriteRenderer.enabled = _stock.Deck.CardsRemaining != 0;


    }
}