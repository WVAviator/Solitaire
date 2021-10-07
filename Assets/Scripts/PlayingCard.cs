using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class PlayingCard : MonoBehaviour, IClickable, IDraggable
    {
        public Card card;
        Stack currentStack;

        bool isFlipped;
        bool isBeingDragged;

        Vector3 homePosition;

        Sprite faceUpSprite;
        SpriteRenderer spriteRenderer;
        CardMovement cardMover;

        public event Action OnCardPicked;
        public event Action OnCardPlaced;

        public void SetCard(Card c)
        {
            card = c;
            spriteRenderer = GetComponent<SpriteRenderer>();
            cardMover = GetComponent<CardMovement>();
            faceUpSprite = CardManager.Instance.GetSprite(c);
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

        public void SetTargetPosition(Vector3 position)
        {
            cardMover.SetNewTargetPosition(position);
        }

        public void Drag(Vector2 updatedPosition)
        {
            if (!isFlipped) return;
            if (IsInDrawStack() && HasChildren()) return;

            SetHome();
            if (!isBeingDragged) OnCardPicked?.Invoke();
            DragCard(updatedPosition);
        }

        bool IsInDrawStack()
        {
            return currentStack.GetType() == typeof(DrawStack);
        }

        void SetHome()
        {
            if (!isBeingDragged) homePosition = transform.position;
        }

        void DragCard(Vector2 updatedPosition)
        {
            transform.position = (Vector3) updatedPosition - Vector3.forward;
            isBeingDragged = true;
            SetLayer(3);
        }

        public void Release(Collider2D col)
        {
            if (!isBeingDragged) return;
            isBeingDragged = false;
            SetLayer(0);

            Stack stackDroppedOn = GetStackFromCollider(col);
            if (stackDroppedOn == null || !stackDroppedOn.CanAddCard(this))
            {
                ResetPosition();
                return;
            }

            OnCardPlaced?.Invoke();
            stackDroppedOn.Transfer(this, currentStack);
        }

        static Stack GetStackFromCollider(Collider2D col)
        {
            if (col.TryGetComponent<Stack>(out Stack s)) return s;
            if (col.TryGetComponent<PlayingCard>(out PlayingCard c)) return c.currentStack;
            return null;
        }

        void ResetPosition()
        {
            SetTargetPosition(homePosition);
        }

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