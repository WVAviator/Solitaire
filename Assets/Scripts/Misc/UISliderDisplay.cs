using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class UISliderDisplay : MonoBehaviour
    {
        [SerializeField] Text _text;
        [SerializeField] Toggle _toggle;
        Slider _slider;

        void Awake() => _slider = GetComponent<Slider>();

        public void UpdateText()
        {
            _text.text = _slider.value.ToString();
            if (_toggle != null) _toggle.isOn = false;
        }
    }
}
