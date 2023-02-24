using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class CheatItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text CheatText;
        
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;
        }

        public Text GetCheatText()
        {
            return CheatText;
        }
    }
}
