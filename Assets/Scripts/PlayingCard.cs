using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class PlayingCard : MonoBehaviour, IClickable, IDraggable
    {
        public CardData CardData => cardData;
        CardData cardData;
        Stack currentStack;

        bool isFlipped;
        bool isBeingDragged;

        Vector3 homePosition;

        Sprite faceUpSprite;
        SpriteRenderer spriteRenderer;
        CardAnimation cardAnimation;

        public event Action OnCardPicked;
        public event Action OnCardPlaced;


        public void SetCard(CardData c)
        {
            cardData = c;
            spriteRenderer = GetComponent<SpriteRenderer>();
            cardAnimation = GetComponent<CardAnimation>();
            faceUpSprite = CardSprites.Instance.GetSprite(c);
        }
        
        public void TurnFaceUp()
        {
            isFlipped = true;
            spriteRenderer.sprite = faceUpSprite;
        }

        public void Click()
        {
            if (!HasChildren() && !isFlipped) TurnFaceUp();
        }
        
        bool HasChildren()
        {
            return transform.childCount != 0;
        }

        public void MoveToPosition(Vector3 position, bool skipAnimation = false)
        {
            if (skipAnimation) transform.position = position;
            else cardAnimation.SetAnimationTargetPosition(position);
            SetHome(position);
        }

        public void Drag(Vector2 updatedPosition, Vector2 clickedPositionOffset)
        {
            if (!isFlipped) return;
            if (IsInWaste() && HasChildren()) return;
            
            if (!isBeingDragged) OnCardPicked?.Invoke();
            DragCard(updatedPosition - clickedPositionOffset);
        }

        bool IsInWaste() => currentStack.GetType() == typeof(WasteStack);
        void SetHome(Vector3 homePosition) => this.homePosition = homePosition;
        

        void DragCard(Vector2 updatedPosition)
        {
            UpdateCardPosition(updatedPosition);
            isBeingDragged = true;
            SetLayer(3);
        }

        void UpdateCardPosition(Vector2 updatedPosition) => transform.position = (Vector3) updatedPosition - Vector3.forward;
        public void Release(Collider2D colliderReleasedOn)
        {
            if (!isBeingDragged) return;
            isBeingDragged = false;
            SetLayer(0);

            Stack stackDroppedOn = GetStackFromCollider(colliderReleasedOn);
            if (!CanBeAddedToStack(stackDroppedOn))
            {
                ResetPosition();
                return;
            }

            OnCardPlaced?.Invoke();
            stackDroppedOn.Transfer(this, currentStack);
        }

        bool CanBeAddedToStack(Stack stackDroppedOn) => stackDroppedOn != null && stackDroppedOn.CanAddCard(this);

        static Stack GetStackFromCollider(Collider2D colliderReleasedOn)
        {
            if (colliderReleasedOn.TryGetComponent(out Stack s)) return s;
            if (colliderReleasedOn.TryGetComponent(out PlayingCard c)) return c.currentStack;
            return null;
        }

        void ResetPosition() => MoveToPosition(homePosition);
        
        void SetLayer(int layerIndex)
        {
            foreach (PlayingCard c in GetComponentsInChildren<PlayingCard>())
            {
                c.gameObject.layer = layerIndex;
            }
        }

        public void SetCurrentStack(Stack stack)
        {
            currentStack = stack;
        }
    }
}