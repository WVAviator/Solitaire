﻿using System;
using UnityEngine;

namespace Solitaire
{
    public class PlayingCard : MonoBehaviour, IClickable, IDraggable
    {
        public CardInfo CardInfo { get; private set; }
        public Stack CurrentStack { get; private set; }

        bool _isBeingDragged;
        Vector3 _homePosition;

        CardAnimation _cardAnimation;
        CardVisuals _cardVisuals;

        public event Action OnCardPicked;
        public event Action OnCardPlaced;

        public void SetCard(CardInfo c)
        {
            CardInfo = c;
            _cardAnimation = GetComponent<CardAnimation>();

            _cardVisuals = gameObject.AddComponent<CardVisuals>();
            _cardVisuals.SetFaceUpSprite(CardInfo);
        }

        public void UpdateCurrentStack(Stack stack)
        {
            CurrentStack = stack;
            transform.SetParent(CurrentStack.GetParent(this));
            MoveToPosition(CurrentStack.GetPosition(this), HasChildren());
        }

        public void Click() => _cardVisuals.TurnFaceUp();

        public void Drag(Vector2 updatedPosition, Vector2 clickedPositionOffset)
        {
            if (!_cardVisuals.IsFlipped) return;
            if (IsInWaste() && HasChildren()) return;

            if (!_isBeingDragged) OnCardPicked?.Invoke();
            DragCard(updatedPosition - clickedPositionOffset);
        }

        public void Release(Collider2D colliderReleasedOn)
        {
            if (!_isBeingDragged) return;
            _isBeingDragged = false;
            SetLayer(0);

            StackTransferRequest stackTransferRequest = new StackTransferRequest(this, colliderReleasedOn);
            if (stackTransferRequest.IsApproved)
            {
                stackTransferRequest.Process();
                OnCardPlaced?.Invoke();
            }
            else ResetPosition();
        }

        void MoveToPosition(Vector3 position, bool skipAnimation = false)
        {
            if (skipAnimation) transform.position = position;
            else _cardAnimation.SetAnimationTargetPosition(position);
            SetHome(position);
        }

        bool HasChildren() => transform.childCount != 0;

        bool IsInWaste() => CurrentStack.GetType() == typeof(WasteStack);

        void SetHome(Vector3 homePosition) => _homePosition = homePosition;


        void DragCard(Vector2 updatedPosition)
        {
            UpdateCardPosition(updatedPosition);
            _isBeingDragged = true;
            SetLayer(3);
        }

        void UpdateCardPosition(Vector2 updatedPosition) =>
            transform.position = (Vector3) updatedPosition - Vector3.forward;

        void ResetPosition() => MoveToPosition(_homePosition);

        void SetLayer(int layerIndex)
        {
            foreach (PlayingCard c in GetComponentsInChildren<PlayingCard>())
            {
                c.gameObject.layer = layerIndex;
            }
        }
    }
}