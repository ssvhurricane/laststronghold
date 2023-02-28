using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class CheatItemDetailView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text CheatDetailText;
            
        private SignalBus _signalBus;

         private bool _isActive = false;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }

         public Text GetCheatDetailText()
        {
            return CheatDetailText;
        }
    }
}
