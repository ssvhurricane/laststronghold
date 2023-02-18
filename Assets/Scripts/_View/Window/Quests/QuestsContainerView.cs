using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class QuestsContainerView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        // TODO:

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }
    }
}