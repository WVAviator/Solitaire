using System;
using UnityEngine;

namespace Solitaire
{
    public class CardVisuals : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;

        public Sprite FaceUpSprite { get; private set; }
        public Sprite FaceDownSprite { get; private set; }

        public bool IsFlipped => _isFlipped;
        bool _isFlipped;
        
        public event Action OnCardFlipped;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            FaceDownSprite = _spriteRenderer.sprite;
        }

        public void SetFaceUpSprite(CardInfo cardInfo) => FaceUpSprite = GetFaceUpSprite(cardInfo);

        static Sprite GetFaceUpSprite(CardInfo cardInfo)
        {
            string spritePath = $"Sprites/Cards/{cardInfo.SuitName}/{cardInfo.RankName}";
            return Resources.Load<Sprite>(spritePath);
        }

        public void TurnFaceUp()
        {
            if (transform.childCount != 0 || _isFlipped) return;
            
            _isFlipped = true;
            _spriteRenderer.sprite = FaceUpSprite;
            OnCardFlipped?.Invoke();
        }

        public void UndoFlip()
        {
            if (!_isFlipped) return;
            _isFlipped = false;
            _spriteRenderer.sprite = FaceDownSprite;
        }
    }
}