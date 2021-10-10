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
            cardPosition.y = transform.position.y - CardSpacing * PlayingCardsInStack.Count;
            cardPosition.z = -0.01f * PlayingCardsInStack.Count;
            
            card.SetTargetPosition(cardPosition);
        }

        public void SetParent(PlayingCard card)
        {
            Transform parent = null;
            int index = 0;
            if (PlayingCardsInStack.Count > 0) parent = PlayingCardsInStack.Last().transform;
            card.transform.parent = parent;
        }

        void RemoveCard(PlayingCard card)
        {
            int numberOfCardsToRemove = PlayingCardsInStack.Count - PlayingCardsInStack.IndexOf(card);
            if (numberOfCardsToRemove == PlayingCardsInStack.Count)
            {
                PlayingCardsInStack.Clear();
                return;
            }

            for (int i = 0; i < numberOfCardsToRemove; i++)
            {
                PlayingCardsInStack.RemoveAt(PlayingCardsInStack.Count - 1);
            }
        }

        public virtual bool CanAddCard(PlayingCard card)
        {
            return false;
        }

        public int CardsInStack()
        {
            return PlayingCardsInStack.Count;
        }

        public void Clear()
        {
            PlayingCardsInStack.Clear();
        }
    }
}