using Services.Localization;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestMenuFlowDetailView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text FlowText;

        [SerializeField] protected Text DescriptionText;

        [SerializeField] protected GameObject QuestContainer;
       
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

        public GameObject GetContainer()
        {
            return QuestContainer;
        }

        public void UpdateView(QuestMenuFlowDetailViewArgs questMenuFlowDetailViewArgs)
        {
              if (_localizationService.HaveKey(questMenuFlowDetailViewArgs.FlowName))
                    FlowText.text = _localizationService.Translate(questMenuFlowDetailViewArgs.FlowName);
            
              if (_localizationService.HaveKey(questMenuFlowDetailViewArgs.DescriptionFlowText))
                    DescriptionText.text = _localizationService.Translate(questMenuFlowDetailViewArgs.DescriptionFlowText);
        }

         public Text GetFlowDetailText()
        {
            return FlowText;
        }

        public void ToggleActive(bool isActive)
        {
            _isActive = isActive;
        }
    }

    public class QuestMenuFlowDetailViewArgs : IWindowArgs
    {
        public string Id { get; set; }

        public string FlowName { get; set; }

        public string DescriptionFlowText { get; set; }

        public QuestMenuFlowDetailViewArgs (){}

        public QuestMenuFlowDetailViewArgs (string id, string flowName, string descriptionFlowText)
        {
            Id = id;

            FlowName = flowName;

            DescriptionFlowText = descriptionFlowText;
        }
    }
}
