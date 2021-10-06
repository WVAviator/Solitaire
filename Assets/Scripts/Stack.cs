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

            SetPosition(card);
            SetParent(card);

            PlayingCardsInStack.Add(card);
            FixCardDisplayOrder();
        }

        protected virtual void SetPosition(PlayingCard card)
        {
            if (PlayingCardsInStack.Count == 0)
            {
                card.transform.position = transform.position;
            }
            else
            {
                card.transform.position =
                    PlayingCardsInStack.Last().transform.position - new Vector3(0, CardSpacing, 0);
            }
        }

        void SetParent(PlayingCard card)
        {
            Transform parent = null;
            if (PlayingCardsInStack.Count > 0) parent = PlayingCardsInStack.Last().transform;
            card.transform.parent = parent;
        }

        void FixCardDisplayOrder()
        {
            for (int i = 0; i < PlayingCardsInStack.Count; i++)
            {
                float zPos = i * -0.01f;
                Vector3 pos = PlayingCardsInStack[i].transform.position;
                PlayingCardsInStack[i].transform.position = new Vector3(pos.x, pos.y, zPos);
            }
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