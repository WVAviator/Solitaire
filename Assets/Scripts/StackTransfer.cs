using UnityEngine;

namespace Solitaire
{
    public class StackTransfer : CardAction
    {
        Stack _newStack;
        Stack _oldStack;
        PlayingCard _card;
        
        public bool IsApproved;
        bool _isProcessed;
        int _revealedIndex = 0;

        public StackTransfer(PlayingCard card, Collider2D releasedCollider)
        {
            _card = card;
            _oldStack = card.CurrentStack;
            _newStack = GetStackFromCollider(releasedCollider);
            IsApproved = CanBeAddedToStack();
            
            if (!(_oldStack is WasteStack stack)) return;
            _revealedIndex = stack.Reveal.IndexOf(_card);
            if (_revealedIndex < 0) _revealedIndex = 0;
        }

        public override void Process()
        {
            if (!IsApproved || _isProcessed) return;

            _newStack.Transfer(_card, _card.CurrentStack);
            _card.UpdateCurrentStack(_newStack);

            _isProcessed = true;
            base.Process();
        }
        bool CanBeAddedToStack() => _newStack != null && _newStack.CanAddCard(_card);
        static Stack GetStackFromCollider(Collider2D colliderReleasedOn)
        {
            if (colliderReleasedOn.TryGetComponent(out Stack s)) return s;
            if (colliderReleasedOn.TryGetComponent(out PlayingCard c)) return c.CurrentStack;
            return null;
        }

        public override void Undo()
        {
            if (_isProcessed == false) return;
            
            _oldStack.Transfer(_card, _card.CurrentStack);
            if (_oldStack is WasteStack stack)
            {
                if (stack.Reveal.Count <= _revealedIndex) stack.AddToRevealedCards(_card);
                else stack.Reveal[_revealedIndex] = _card;
            }
            _card.UpdateCurrentStack(_oldStack);

            _isProcessed = false;
        }
    }
}