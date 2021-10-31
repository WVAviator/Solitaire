using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public abstract class Stack : MonoBehaviour
    {
        public List<PlayingCard> CardStack = new List<PlayingCard>();
        
        [SerializeField] protected float CardSpacing = 0.35f;

        protected virtual void OnEnable() => Stock.OnNewGameDeal += Clear;
        protected virtual void OnDisable() => Stock.OnNewGameDeal -= Clear;

        public void Transfer(PlayingCard card, Stack oldStack)
        {
            card.UpdateCurrentStack(this);
            
            Add(card);
            if (oldStack != null) oldStack.Remove(card);
            
            AssignPosition(card);
            AssignParent(card);
        }

        public virtual bool CanAddCard(PlayingCard card) => false;

        public virtual void AssignPosition(PlayingCard card)
        {
            int cardIndex = CardStack.IndexOf(card);

            Vector3 cardPosition;

            cardPosition.x = transform.position.x;
            cardPosition.y = YPositionWithSpacing(cardIndex);
            cardPosition.z = ZPositionWithLayering(cardIndex);

            card.MoveToPosition(cardPosition);
        }

        public void AssignParent(PlayingCard card)
        {
            Transform parent = null;

            int cardIndex = CardStack.IndexOf(card);
            if (cardIndex > 0) parent = CardStack[cardIndex - 1].transform;

            card.transform.parent = parent;
        }

        public virtual void Add(PlayingCard card) => CardStack.Add(card);
        protected float ZPositionWithLayering(int spacedBy) => -0.01f * spacedBy;
        protected float YPositionWithSpacing(int spacedBy) => transform.position.y - CardSpacing * spacedBy;

        protected void Clear()
        {
            foreach (PlayingCard pc in CardStack) Destroy(pc.gameObject);
            CardStack.Clear();
        }

        public void Remove(PlayingCard card)
        {
            foreach (PlayingCard c in card.GetComponentsInChildren<PlayingCard>()) CardStack.Remove(c);
        }
    }
}