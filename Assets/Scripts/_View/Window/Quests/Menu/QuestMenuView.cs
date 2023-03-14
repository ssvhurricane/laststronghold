using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/QuestMenuView", WindowType.PopUpWindow)]
    public class QuestMenuView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected CreationMethod Method;
        [SerializeField] protected GameObject LeftContainer;
        [SerializeField] protected GameObject RightContainer;
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

        public GameObject GetLeftContainer()
        {
            return LeftContainer;
        }

        public GameObject GetRightcontainer()
        {
            return RightContainer;
        }
    }
}
