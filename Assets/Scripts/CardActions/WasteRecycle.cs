using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class WasteRecycle : CardAction
    {
        Stock _stock;
        WasteStack _wasteStack;

        int _priorRevealCount;

        public WasteRecycle(Stock stock, WasteStack wasteStack)
        {
            _stock = stock;
            _wasteStack = wasteStack;

            _priorRevealCount = _wasteStack.RevealedCardsStillInStack();
        }

        public override void Process()
        {
            RecycleReveal();
            _stock.StockPassesRemaining--;
            
            base.Process();
        }

        void RecycleReveal()
        {
            Deck deck = new Deck(_wasteStack.GetRecycledStock());
            _stock.SetDeck(deck);
        }


        public override void Undo()
        {
            _stock.RestoreWaste();
            _wasteStack.RevealLastInStack(_priorRevealCount);

            _stock.StockPassesRemaining++;
        }
    }
}