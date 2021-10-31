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
            AddCard(card);
            if (oldStack != null) oldStack.RemoveCard(card);
        }

        public virtual bool CanAddCard(PlayingCard card) => false;

        public virtual Vector3 GetPosition(PlayingCard card)
        {
            int cardIndex = CardStack.IndexOf(card);

            Vector3 cardPosition;

            cardPosition.x = transform.position.x;
            cardPosition.y = YPositionWithSpacing(cardIndex);
            cardPosition.z = ZPositionWithLayering(cardIndex);

            return cardPosition;
        }

        public Transform GetParent(PlayingCard card)
        {
            Transform parent = null;

            int cardIndex = CardStack.IndexOf(card);
            if (cardIndex > 0) parent = CardStack[cardIndex - 1].transform;

            return parent;
        }

        public virtual void AddCard(PlayingCard card) => CardStack.Add(card);
        protected float ZPositionWithLayering(int spacedBy) => -0.01f * spacedBy;
        protected float YPositionWithSpacing(int spacedBy) => transform.position.y - CardSpacing * spacedBy;

        protected void Clear()
        {
            foreach (PlayingCard pc in CardStack) Destroy(pc.gameObject);
            CardStack.Clear();
        }

        public void RemoveCard(PlayingCard card)
        {
            foreach (PlayingCard c in card.GetComponentsInChildren<PlayingCard>()) CardStack.Remove(c);
        }
    }
}