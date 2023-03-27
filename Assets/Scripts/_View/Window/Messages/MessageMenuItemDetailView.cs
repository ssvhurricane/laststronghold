using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class MessageMenuItemDetailView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text MessageDetailText;

        [SerializeField] protected GameObject Container;
            
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

        public Text GetMessageDetailText()
        {
            return MessageDetailText;
        }

        public GameObject GetContainer()
        {
            return Container;
        }
         public void UpdateView(MessageMenuItemDetailViewArgs messageMenuItemDetailViewArgs)
        {
            Id = messageMenuItemDetailViewArgs.Id;

            // TODO:
        }
    }

     public class MessageMenuItemDetailViewArgs : IWindowArgs
    {
        public string Id { get ; set; }

         public string Name { get; set; }

         public MessageMenuItemDetailViewArgs(){}

         public MessageMenuItemDetailViewArgs(string id, string name)
         {
            Id = id;

            Name = name;
         }  
    }
}

