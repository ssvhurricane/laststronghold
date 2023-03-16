using Services.Localization;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Signals;

namespace View.Window
{
    public class QuestMenuFlowView : WindowItem
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text QuestMenuFlowText;

        [SerializeField] protected Button QuestMenuButton;

        private SignalBus _signalBus; 
        private LocalizationService _localizationService;

        private bool _isActive = false;

        [Inject]
        public void Constrcut(SignalBus signalBus, LocalizationService localizationService)
        {
            _signalBus = signalBus;

            _localizationService = localizationService;

            WindowType = Type;

            QuestMenuButton.onClick.AddListener(OnActivate);
        }

        private void OnActivate()
        {
            _signalBus.Fire(new QuestServiceSignals.ActivateQuestMenuFlowView(QuestMenuFlowText.text));
        }

        public void UpdateView(QuestMenuFlowViewArgs questMenuFlowViewArgs)
        {
            Id = questMenuFlowViewArgs.Id;

            if (_localizationService.HaveKey(questMenuFlowViewArgs.Name))
                QuestMenuFlowText.text = _localizationService.Translate(questMenuFlowViewArgs.Name);
        }

        public Text GetFlowText()
        {
            return QuestMenuFlowText;
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
