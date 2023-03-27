using Services.Localization;
using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class MessageItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text MessageDescription;

        [SerializeField] protected Text MessageDate;

        private SignalBus _signalBus;

        private LocalizationService _localizationService;

        private bool _isActive = false;

        [Inject]
        public void Constrcut(SignalBus signalBus, LocalizationService localizationService)
        {
            _signalBus = signalBus;

            _localizationService = localizationService;

            WindowType = Type;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void UpdateView(MessageItemViewArgs messageItemViewArgs)
        {
            Id = messageItemViewArgs.Id;

              if(_localizationService.HaveKey(messageItemViewArgs.Description))
                MessageDescription.text =_localizationService.Translate(messageItemViewArgs.Description);

            MessageDate.text = messageItemViewArgs.Date;

        }
    }

    public class MessageItemViewArgs : IWindowArgs
    {
        public string Id { get ; set; }

         public string Name { get; set; }

         public string Description { get; set; }

         public string Date { get; set ;}

         public MessageItemViewArgs(){}

         public MessageItemViewArgs(string id, string name, string description, string date)
         {
            Id = id;

            Name = name;

            Description = description;

            Date = date;
         }  
    }
}
