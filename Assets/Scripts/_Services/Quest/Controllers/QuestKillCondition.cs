using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestKillCondition : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestKillCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestKillEvent>(signal => OnQuestKill());
        }

        private void OnQuestKill()
        {
            // TODO:
        }
    }
}