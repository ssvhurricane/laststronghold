using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestCollectCondition : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestCollectCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestCollectEvent>(signal => OnQuestCollect());
        }

        private void OnQuestCollect()
        {
            // TODO:
        }
    }
}
