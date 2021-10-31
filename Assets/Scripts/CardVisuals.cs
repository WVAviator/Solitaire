using System;
using UnityEngine;

namespace Solitaire
{
    public class CardVisuals : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;

        public Sprite FaceUpSprite { get; private set; }
        public Sprite FaceDownSprite { get; private set; }

        public bool IsFlipped { get; private set; }

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
            IsFlipped = true;
            _spriteRenderer.sprite = FaceUpSprite;
            OnCardFlipped?.Invoke();
        }

        public void TurnFaceDown()
        {
            IsFlipped = false;
            _spriteRenderer.sprite = FaceDownSprite;
        }
    }
}