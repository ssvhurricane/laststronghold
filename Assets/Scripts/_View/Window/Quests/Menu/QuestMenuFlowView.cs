using Services.Localization;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestMenuFlowView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text QuestMenuFlowText;

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

         public void UpdateView(QuestMenuFlowViewArgs questMenuFlowViewArgs)
         {
            Id = questMenuFlowViewArgs.Id;

            if (_localizationService.HaveKey(questMenuFlowViewArgs.Name))
                QuestMenuFlowText.text = _localizationService.Translate(questMenuFlowViewArgs.Name);
         }
    }

    public class QuestMenuFlowViewArgs : IWindowArgs
    {
         public string Id { get ; set; }

         public string Name { get; set; }

         public QuestMenuFlowViewArgs(){}

         public QuestMenuFlowViewArgs(string id, string name)
         {
            Id = id;

            Name = name;
         }
    }
}
