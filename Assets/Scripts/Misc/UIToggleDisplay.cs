using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class UIToggleDisplay : MonoBehaviour
    {

        [SerializeField] Text _infiniteText;
        [SerializeField] Slider _slider;
        Toggle _toggle;

        void Awake() => _toggle = GetComponent<Toggle>();

        public void UpdateDisplay(bool value)
        {
            if (_toggle.isOn) _infiniteText.text = "∞";
            else _infiniteText.text = _slider.value.ToString();
        }
    }
}
