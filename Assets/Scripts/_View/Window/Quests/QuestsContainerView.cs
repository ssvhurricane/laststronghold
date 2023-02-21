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
        [SerializeField] protected QuestItemView QuestItemView;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

             WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }

        public void UpdateView(List<QuestsContainerViewArgs> args)
        {
            // TODO:
        }
    } 

    public class QuestsContainerViewArgs : IWindowArgs 
    {
        public int Id { get; set; }

        public string Description {get; set; }

        public QuestState QuestState { get; set; }

        public int Value { get; set; }

        public int MaxValue { get; set; } 
    }
}