
using Services.Quest;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Services.Localization;

namespace View.Window
{
    public class QuestItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;
        [SerializeField] protected Image QuestCompleteImage;
        [SerializeField] protected Image QuestImage;
        [SerializeField] protected Text QuestText;

        private SignalBus _signalBus;

        private LocalizationService _localizationService;

        private Image _image;

        private int _questId;

        [Inject]
        public void Constrcut(SignalBus signalBus,
                                 LocalizationService localizationService)
        {
            _signalBus = signalBus;

            _localizationService = localizationService;

            _image = GetComponent<Image>();

            WindowType = Type;
        }

        public void UpdateView(QuestItemViewArgs questItemViewArgs)
        {  
             Id = questItemViewArgs.Id.ToString();
             
            _questId = questItemViewArgs.Id;

            var countItem = questItemViewArgs.NeedValue != 0 ? " " + questItemViewArgs.CurrentValue.ToString() + "/" + questItemViewArgs.NeedValue:string.Empty; 

            if (_localizationService.HaveKey(questItemViewArgs.Description))
                QuestText.text = _localizationService.Translate(questItemViewArgs.Description) + countItem;

            switch(questItemViewArgs.QuestState)
            {
                case QuestState.Active:
                {
                    QuestCompleteImage.gameObject.SetActive(false);
                    break;
                }
                case QuestState.Complete:
                { 
                    QuestCompleteImage.gameObject.SetActive(true);
                    break;
                }
               

            }
        }
    }

    public class QuestItemViewArgs : IWindowArgs 
    {
        public int Id { get; set; }

        public string Description {get; set; }

        public QuestState QuestState { get; set; }

        public int CurrentValue { get; set; }

        public int NeedValue { get; set; } 

        public QuestItemViewArgs(){}

        public QuestItemViewArgs(int id, string description, QuestState questState, int currentValue, int needValue)
        {
            Id = id;

            Description = description;

            QuestState = questState;

            CurrentValue = currentValue;

            NeedValue = needValue;
        }
    }
}
