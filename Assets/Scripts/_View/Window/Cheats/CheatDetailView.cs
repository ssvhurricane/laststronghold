using Services.Window;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class CheatDetailView : WindowItem
    {
        [SerializeField] protected WindowType Type;
            
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;
        }
    }
}
