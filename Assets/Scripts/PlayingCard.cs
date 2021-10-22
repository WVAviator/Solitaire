using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class PlayingCard : MonoBehaviour, IClickable, IDraggable
    {
        public CardInfo CardInfo => _cardInfo;
        CardInfo _cardInfo;
        Stack _currentStack;

        bool _isFlipped;
        bool _isBeingDragged;

        Vector3 _homePosition;

        Sprite _faceUpSprite;
        SpriteRenderer _spriteRenderer;
        CardAnimation _cardAnimation;
        CardVisuals _cardVisuals;

        public event Action OnCardPicked;
        public event Action OnCardPlaced;



        public void SetCard(CardInfo c)
        {
            _cardInfo = c;
            _cardAnimation = GetComponent<CardAnimation>();
            
            _cardVisuals = gameObject.AddComponent<CardVisuals>();
            _cardVisuals.SetFaceUpSprite(_cardInfo);
        }

        public void Click() => _cardVisuals.TurnFaceUp();

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
        
        bool HasChildren() => transform.childCount != 0;
        bool IsInWaste() => _currentStack.GetType() == typeof(WasteStack);
        void SetHome(Vector3 homePosition) => _homePosition = homePosition;
        

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