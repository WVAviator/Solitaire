using UnityEngine;

namespace Solitaire
{
    public class StackTransferRequest
    {
        Stack _newStack;
        PlayingCard _card;
        
        public bool IsApproved;

        public StackTransferRequest(PlayingCard card, Collider2D releasedCollider)
        {
            _card = card;
            _newStack = GetStackFromCollider(releasedCollider);
            IsApproved = CanBeAddedToStack();
        }

        public StackTransferRequest(PlayingCard card, Stack newStack, bool overrideLegality = false)
        {
            _card = card;
            _newStack = newStack;
            IsApproved = overrideLegality || CanBeAddedToStack();
        }

        public void Process()
        {
            if (!IsApproved) return;

            _newStack.Transfer(_card, _card.CurrentStack);
            _card.UpdateCurrentStack(_newStack);
        }
        bool CanBeAddedToStack() => _newStack != null && _newStack.CanAddCard(_card);
        static Stack GetStackFromCollider(Collider2D colliderReleasedOn)
        {
            if (colliderReleasedOn.TryGetComponent(out Stack s)) return s;
            if (colliderReleasedOn.TryGetComponent(out PlayingCard c)) return c.CurrentStack;
            return null;
        }
    }
}