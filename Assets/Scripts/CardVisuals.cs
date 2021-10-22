using System;
using UnityEngine;

namespace Solitaire
{
    public class CardVisuals : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Sprite _faceUpSprite;
        bool _isFlipped;
        
        public event Action OnCardFlipped;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void SetFaceUpSprite(CardInfo cardInfo)
        {
            string spritePath = $"Sprites/Cards/{cardInfo.SuitName}/{cardInfo.RankName}";
            _faceUpSprite = Resources.Load<Sprite>(spritePath);
        }

        public void TurnFaceUp()
        {
            if (transform.childCount != 0 || _isFlipped) return;
            
            _isFlipped = true;
            _spriteRenderer.sprite = _faceUpSprite;
            OnCardFlipped?.Invoke();
        }
    }
}