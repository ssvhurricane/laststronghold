using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class CheatItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text CheatText;

        [SerializeField] protected Button CheatMenubutton;
        
        private SignalBus _signalBus;

        private bool _isActive = false;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            CheatMenubutton.onClick.AddListener(OnActivate);
        }

        private void OnActivate()
        {
            _signalBus.Fire(new CheatServiceSignals.ActivateCheatItemView(CheatText.text));
        }

        public Text GetCheatText()
        {
            return CheatText;
        }

        public Button GetCheatMenuButton()
        {
            return CheatMenubutton;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }
    }
}
