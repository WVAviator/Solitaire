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
            IsApproved = CanBeAddedToStack;
            
            ManageWasteReveal();
        }

        void ManageWasteReveal()
        {
            if (!(_oldStack is WasteStack stack)) return;
            _revealedIndex = stack.Reveal.IndexOf(_card);
            if (_revealedIndex < 0) _revealedIndex = 0;
        }

        public override void Process()
        {
            if (!IsApproved || _isProcessed) return;

            _newStack.Transfer(_card, _card.CurrentStack);

            _isProcessed = true;
            base.Process();
        }

        bool CanBeAddedToStack => _newStack != null && _newStack.CanAddCard(_card);

        static Stack GetStackFromCollider(Collider2D colliderReleasedOn)
        {
            if (colliderReleasedOn.TryGetComponent(out Stack s)) return s;
            if (colliderReleasedOn.TryGetComponent(out PlayingCard c)) return c.CurrentStack;
            return null;
        }

        public override void Undo()
        {
            if (_isProcessed == false) return;
            
            SendBackToWasteReveal();
            _oldStack.Transfer(_card, _card.CurrentStack);

            _isProcessed = false;
        }

        void SendBackToWasteReveal()
        {
            if (!(_oldStack is WasteStack stack)) return;
            if (stack.Reveal.Count <= _revealedIndex) stack.AddToRevealedCards(_card);
            else stack.Reveal[_revealedIndex] = _card;
        }
    }
}