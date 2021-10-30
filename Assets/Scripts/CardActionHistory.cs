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
            FoundationStack.OnAllFoundationsComplete += ClearHistory;
        }

        void OnDisable()
        {
            CardAction.OnCardActionPerformed -= AddAction;
            Stock.OnNewGameDeal -= ClearHistory;
        }

        public void UndoAction()
        {
            if (_actions.Count == 0) return;
            _actions.Pop().Undo();
        }

        void AddAction(CardAction action) => _actions.Push(action);

        void ClearHistory(List<FoundationStack> foundations) => ClearHistory();

        void ClearHistory() => _actions.Clear();
    }
}