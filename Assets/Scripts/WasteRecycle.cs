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

            _priorRevealCount = _wasteStack.NumberOfRevealedCards();
        }

        public override void Process()
        {
            Deck deck = new Deck(_wasteStack.GetRecycledStock());
            _stock.SetDeck(deck);

            _stock.StockPassesRemaining--;
            
            base.Process();
        }


        public override void Undo()
        {
            _stock.RestoreWaste();
            _wasteStack.ResortReveal(_priorRevealCount);

            _stock.StockPassesRemaining++;
        }
    }
}