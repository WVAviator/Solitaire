using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    public class CardActionHistory : MonoBehaviour
    {
        Stack<CardAction> _actions;

        void Awake() => _actions = new Stack<CardAction>();

        void OnEnable()
        {
            CardAction.OnCardActionPerformed += AddAction;
            Stock.OnNewGameDeal += ClearHistory;
        }
        void OnDisable()
        {
            CardAction.OnCardActionPerformed -= AddAction;
            Stock.OnNewGameDeal -= ClearHistory;
        }
        void AddAction(CardAction action) => _actions.Push(action);
        void ClearHistory() => _actions.Clear();

        public void UndoAction()
        {
            if (_actions.Count == 0) return;
            _actions.Pop().Undo();
        }
    }
}