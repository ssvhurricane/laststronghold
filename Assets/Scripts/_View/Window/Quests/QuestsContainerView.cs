using System.Collections.Generic;
using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class QuestsContainerView : BaseWindow
    {
        [SerializeField] protected WindowType Type;
        [SerializeField] protected GameObject QuestContainer;
        [SerializeField] protected Text FlowText;
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

             WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }

        public void AttachView(List<QuestItemView> QuestItemViews)
        {
            if (QuestItemViews != null)
                foreach(var questItemView in QuestItemViews)
                    questItemView.gameObject.transform.parent = QuestContainer.transform;
        }

        public void UpdateView(QuestsContainerViewArgs questsContainerViewArgs)
        {
            FlowText.text = questsContainerViewArgs.FlowDescriptionText;
        }

        public GameObject GetQuestContainer()
        {
            return QuestContainer;
        }
    } 

    public class QuestsContainerViewArgs : IWindowArgs
    {
        public string FlowDescriptionText { get; set; }
        public QuestsContainerViewArgs(){}

         public QuestsContainerViewArgs(string flowDescriptionText)
         {
            FlowDescriptionText = flowDescriptionText;
         }
    }
}