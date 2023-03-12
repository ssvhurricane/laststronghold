using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestUpgradeCondition : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestUpgradeCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestUpgradeEvent>(signal => OnQuestUpgrade());
        }

        private void OnQuestUpgrade()
        {
            // TODO:
        }
    }
}
