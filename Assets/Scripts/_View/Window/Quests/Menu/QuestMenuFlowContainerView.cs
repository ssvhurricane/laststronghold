using Services.Localization;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestMenuFlowContainerView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text QuestMenuFlowContainerText;

        [SerializeField] protected GameObject FlowContainer;
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

         public void UpdateView(QuestMenuFlowContainerViewArgs questMenuFlowContainerViewArgs)
         {
            Id = questMenuFlowContainerViewArgs.Id;

            if (_localizationService.HaveKey(questMenuFlowContainerViewArgs.Name))
                 QuestMenuFlowContainerText.text = _localizationService.Translate(questMenuFlowContainerViewArgs.Name);
         }  
         
        public GameObject GetContainer()
        {
            return FlowContainer;
        }
    }

    public class QuestMenuFlowContainerViewArgs : IWindowArgs
    {
         public string Id { get ; set; }

         public string Name { get; set; }

         public QuestMenuFlowContainerViewArgs(){}

         public QuestMenuFlowContainerViewArgs(string id, string name)
         {
            Id = id;

            Name = name;
         }
    }
}