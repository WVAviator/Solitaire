using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Solitaire
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] GameObject _settingsPanel;
        public static event Action<Settings> OnSettingsUpdated;
        
        public int DrawCount { get; private set; } = 3;
        public int StockPasses { get; private set;} = 3;
        public bool InfiniteStockPasses { get;private set; } = true;
        public bool UndoAllowed { get; private set;} = false;

        [SerializeField] Slider _drawAmountSlider;
        [SerializeField] Toggle _infinitePassesToggle;
        [SerializeField] Slider _passesSlider;
        [SerializeField] Toggle _undoToggle;

        public void OpenSettingsPanel() => _settingsPanel.SetActive(true);
        public void CloseSettingsPanel() => _settingsPanel.SetActive(false);

        public void ApplySettings()
        {
            DrawCount = Mathf.RoundToInt(_drawAmountSlider.value);
            InfiniteStockPasses = _infinitePassesToggle.isOn;
            StockPasses = InfiniteStockPasses ? int.MaxValue : Mathf.RoundToInt(_passesSlider.value) - 1;
            UndoAllowed = _undoToggle.isOn;
            
            OnSettingsUpdated?.Invoke(this);
            CloseSettingsPanel();
            FindObjectOfType<Stock>().DealNewGame();
        }
    }
}