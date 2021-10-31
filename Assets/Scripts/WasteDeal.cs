using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solitaire
{
    public class WasteDeal : CardAction
    {
        Stock _stock;
        WasteStack _wasteStack;
        PlayingCard[] _cards;
        
        int _numberOfCardsToDraw;
        int _priorRevealCount;
        
        public WasteDeal(Stock stock, WasteStack wasteStack, PlayingCard[] cards)
        {
            _stock = stock;
            _wasteStack = wasteStack;
            _cards = cards;

            _numberOfCardsToDraw = cards.Length;
            _priorRevealCount = _wasteStack.RevealedCardsStillInStack();
        }
        
        public override void Process()
        {
            _wasteStack.ResetRevealedCards();

            foreach (PlayingCard card in _cards)
            {
                _wasteStack.AddToRevealedCards(card);
                _wasteStack.AddCard(card);
                card.UpdateCurrentStack(_wasteStack);
            }
            
            base.Process();
        }

        public override void Undo()
        {
            for (int i = 0; i < _numberOfCardsToDraw; i++)
            {
                PlayingCard card = _wasteStack.CardStack.Last();
                _wasteStack.RemoveCard(card);
                _stock.ReturnToDeck(card);
            }

            _wasteStack.RevealLastInStack(_priorRevealCount);
        }
    }
}