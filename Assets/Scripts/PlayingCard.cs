using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class PlayingCard : MonoBehaviour, IClickable, IDraggable
    {
        public CardData CardData => _cardData;
        CardData _cardData;
        Stack _currentStack;

        bool _isFlipped;
        bool _isBeingDragged;

        Vector3 _homePosition;

        Sprite _faceUpSprite;
        SpriteRenderer _spriteRenderer;
        CardAnimation _cardAnimation;

        public event Action OnCardPicked;
        public event Action OnCardPlaced;


        public void SetCard(CardData c)
        {
            _cardData = c;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _cardAnimation = GetComponent<CardAnimation>();
            _faceUpSprite = CardSprites.Instance.GetSprite(c);
        }
        
        public void TurnFaceUp()
        {
            _isFlipped = true;
            _spriteRenderer.sprite = _faceUpSprite;
        }

        public void Click()
        {
            if (!HasChildren() && !_isFlipped) TurnFaceUp();
        }
        
        bool HasChildren()
        {
            return transform.childCount != 0;
        }

        public void MoveToPosition(Vector3 position, bool skipAnimation = false)
        {
            if (skipAnimation) transform.position = position;
            else _cardAnimation.SetAnimationTargetPosition(position);
            SetHome(position);
        }

        public void Drag(Vector2 updatedPosition, Vector2 clickedPositionOffset)
        {
            if (!_isFlipped) return;
            if (IsInWaste() && HasChildren()) return;
            
            if (!_isBeingDragged) OnCardPicked?.Invoke();
            DragCard(updatedPosition - clickedPositionOffset);
        }

        bool IsInWaste() => _currentStack.GetType() == typeof(WasteStack);
        void SetHome(Vector3 homePosition) => this._homePosition = homePosition;
        

        void DragCard(Vector2 updatedPosition)
        {
            UpdateCardPosition(updatedPosition);
            _isBeingDragged = true;
            SetLayer(3);
        }

        void UpdateCardPosition(Vector2 updatedPosition) => transform.position = (Vector3) updatedPosition - Vector3.forward;
        public void Release(Collider2D colliderReleasedOn)
        {
            if (!_isBeingDragged) return;
            _isBeingDragged = false;
            SetLayer(0);

            Stack stackDroppedOn = GetStackFromCollider(colliderReleasedOn);
            if (!CanBeAddedToStack(stackDroppedOn))
            {
                ResetPosition();
                return;
            }

            OnCardPlaced?.Invoke();
            stackDroppedOn.Transfer(this, _currentStack);
        }

        bool CanBeAddedToStack(Stack stackDroppedOn) => stackDroppedOn != null && stackDroppedOn.CanAddCard(this);

        static Stack GetStackFromCollider(Collider2D colliderReleasedOn)
        {
            if (colliderReleasedOn.TryGetComponent(out Stack s)) return s;
            if (colliderReleasedOn.TryGetComponent(out PlayingCard c)) return c._currentStack;
            return null;
        }

        void ResetPosition() => MoveToPosition(_homePosition);
        
        void SetLayer(int layerIndex)
        {
            foreach (PlayingCard c in GetComponentsInChildren<PlayingCard>())
            {
                c.gameObject.layer = layerIndex;
            }
        }

        public void SetCurrentStack(Stack stack)
        {
            _currentStack = stack;
        }
    }
}