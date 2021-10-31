using System;

namespace Solitaire
{
    public abstract class CardAction
    {
        public static event Action<CardAction> OnCardActionPerformed;
        
        public virtual void Process() => OnCardActionPerformed?.Invoke(this);
        public abstract void Undo();
    }
}