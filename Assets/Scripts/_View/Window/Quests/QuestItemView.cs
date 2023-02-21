
using Services.Quest;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestItemView : WindowItem
    {
        [SerializeField] protected WindowType Type;
        [SerializeField] protected Image QuestCompleteImage;
        [SerializeField] protected Image QuestImage;
        [SerializeField] protected Text QuestText;

        private SignalBus _signalBus;

        private Image _image;

        private int _questId;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _image = GetComponent<Image>();

            WindowType = Type;
        }

        public void UpdateView(QuestItemViewArgs questItemViewArgs)
        {  
            _questId = questItemViewArgs.Id;

            var countItem = questItemViewArgs.MaxValue != 0 ? " " + questItemViewArgs.Value.ToString() + "/" + questItemViewArgs.MaxValue:string.Empty; 

            QuestText.text = questItemViewArgs.Description + countItem;

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

        public int Value { get; set; }

        public int MaxValue { get; set; } 
    }
}
