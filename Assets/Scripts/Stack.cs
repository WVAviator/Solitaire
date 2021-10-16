using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public abstract class Stack : MonoBehaviour
    {
        protected readonly List<PlayingCard> PlayingCardsInStack = new List<PlayingCard>();
        [SerializeField] protected float CardSpacing = 0.35f;

        void OnEnable() => GameManager.OnTableClear += Clear;
        void OnDisable() => GameManager.OnTableClear -= Clear;
        
        public void Transfer(PlayingCard card, Stack oldStack)
        {
            Stack newStack = this;

            newStack.AddCard(card);
            oldStack.RemoveCard(card);
        }
        public virtual void AddCard(PlayingCard card)
        {
            card.SetCurrentStack(this);
            SetParent(card);
            SetPosition(card);
            PlayingCardsInStack.Add(card);
        }
        protected virtual void SetPosition(PlayingCard card)
        {
            Vector3 cardPosition;

            cardPosition.x = transform.position.x;
            cardPosition.y = YPositionWithSpacing(PlayingCardsInStack.Count);
            cardPosition.z = ZPositionWithLayering();
            
            card.MoveToPosition(cardPosition, card.transform.childCount > 0);
        }
        protected float ZPositionWithLayering() => -0.01f * PlayingCardsInStack.Count;
        protected float YPositionWithSpacing(int spacedBy) => transform.position.y - CardSpacing * spacedBy;

        void SetParent(PlayingCard card)
        {
            Transform parent = null;
            if (PlayingCardsInStack.Count > 0) parent = PlayingCardsInStack.Last().transform;
            card.transform.SetParent(parent);
        }

        void RemoveCard(PlayingCard card)
        {
            foreach (PlayingCard c in card.GetComponentsInChildren<PlayingCard>()) PlayingCardsInStack.Remove(c);
            PlayingCardsInStack.Remove(card);
        }

        public virtual bool CanAddCard(PlayingCard card) => false;
        public int CardsInStack() => PlayingCardsInStack.Count;

        protected void Clear()
        {
            foreach (PlayingCard pc in PlayingCardsInStack) Destroy(pc.gameObject);
            PlayingCardsInStack.Clear();
        }
        
    }
}