using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Services.Cheat
{
    public class CheatButtonControl : CheatItemControl
    {
        [SerializeField] private Text _buttonLabel;
        [SerializeField] private Button _button;

        public CheatButtonControl SetButtonName(string buttonName)
        {
            _buttonLabel.text = buttonName;
            return this;
        }

        public CheatButtonControl SetButtonCallback(UnityAction callback,bool forceClose = false)
        {
            _button?.onClick.AddListener(callback);

            return this;
        }
    }
}