using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/CheatSettingsView", WindowType.PopUpWindow)]
    public class CheatSettingsView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected CreationMethod Method;
        [SerializeField] public Button _backToGameButton;
        
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            CreationMethod = Method;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }
    }
}
