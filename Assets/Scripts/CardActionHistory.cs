using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class CardActionHistory : MonoBehaviour
    {
        [SerializeField] Button _undoButton;
        
        Stack<CardAction> _actions;
        bool _undoAllowed;

        void Awake() => _actions = new Stack<CardAction>();

        void OnEnable()
        {
            CardAction.OnCardActionPerformed += AddAction;
            Stock.OnNewGameDeal += ClearHistory;
            FoundationStack.OnAllFoundationsComplete += ClearHistory;
            Settings.OnSettingsUpdated += UpdateSettings;
        }

        void OnDisable()
        {
            CardAction.OnCardActionPerformed -= AddAction;
            Stock.OnNewGameDeal -= ClearHistory;
            FoundationStack.OnAllFoundationsComplete -= ClearHistory;
            Settings.OnSettingsUpdated -= UpdateSettings;
        }
        void UpdateSettings(Settings settings) => _undoAllowed = settings.UndoAllowed;

        void UpdateButtonInteractable() => _undoButton.interactable = _undoAllowed && _actions.Count != 0;

        public void UndoAction()
        {
            if (_actions.Count == 0 || !_undoAllowed) return;
            _actions.Pop().Undo();
            UpdateButtonInteractable();
        }

        void AddAction(CardAction action)
        {
            _actions.Push(action);
            UpdateButtonInteractable();
        }

        void ClearHistory(List<FoundationStack> foundations) => ClearHistory();

        void ClearHistory()
        {
            _actions.Clear();
            UpdateButtonInteractable();
        }
    }
}