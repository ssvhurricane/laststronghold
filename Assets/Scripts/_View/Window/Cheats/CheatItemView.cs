using Services.Window;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class CheatItemView : WindowItem
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
