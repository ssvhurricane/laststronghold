using System.Collections.Generic;
using Services.Quest;
using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class QuestsContainerView : BaseWindow
    {
        [SerializeField] protected WindowType Type;
        [SerializeField] protected GameObject QuestContainer;
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
    } 
}