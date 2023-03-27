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

        [SerializeField] protected Text MessageMenuOwnerName;

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
            _signalBus.Fire(new MessageServiceSignals.ActivateMessageItemView(MessageMenuOwnerName.text));
        }

        public Text GetMessageMenuOwmerNameText()
        {
            return  MessageMenuOwnerName;
        }

        public Button GetMessageMenuButton()
        {
            return MessageMenuButton;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void UpdateView(MessageMenuItemViewArgs messageMenuItemViewArgs)
        {
            Id = messageMenuItemViewArgs.Id;

            // TODO:
        }
    }

    public class MessageMenuItemViewArgs : IWindowArgs
    {
        public string Id { get ; set; }

         public string Name { get; set; }

         public MessageMenuItemViewArgs(){}

         public MessageMenuItemViewArgs(string id, string name)
         {
            Id = id;

            Name = name;
         }  
    }
}
