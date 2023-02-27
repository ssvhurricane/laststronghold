using Services.Window;
using UnityEngine;
using View.Window;
using Zenject;

namespace Services.Cheat
{
    public abstract class CheatItemControl : WindowItem 
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