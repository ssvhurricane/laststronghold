using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class MessageMenuItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text MessageMenuText;

        [SerializeField] protected Image OwnerImage;

        [SerializeField] protected Text OwnerDescription;

        [SerializeField] protected Button MessageMenuButton;
        
        private SignalBus _signalBus;

        private bool _isActive = false;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            MessageMenuButton.onClick.AddListener(OnActivate);
        }

        private void OnActivate()
        {
            _signalBus.Fire(new MessageServiceSignals.ActivateMessageItemView(MessageMenuText.text));
        }

        public Text GetMessageMenuText()
        {
            return MessageMenuText;
        }

        public Button GetMessageMenuButton()
        {
            return MessageMenuButton;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }
    }
}
