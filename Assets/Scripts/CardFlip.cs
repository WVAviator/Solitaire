namespace Solitaire
{
    public class CardFlip : CardAction
    {
        readonly CardVisuals _cardVisuals;
        public CardFlip(CardVisuals cardVisuals)
        {
            _cardVisuals = cardVisuals;
        }
        public override void Process()
        {
            _cardVisuals.TurnFaceUp();
            if (!_cardVisuals.IsFlipped) return;
            
            base.Process();
        }
        public override void Undo() => _cardVisuals.TurnFaceDown();
    }
}