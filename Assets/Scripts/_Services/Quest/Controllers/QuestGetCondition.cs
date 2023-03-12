using System;
using Signals;
using Zenject;

namespace Services.Quest
{
    public class QuestGetCondition : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestGetCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestGetEvent>(signal => OnQuestGet());
        }

        private void OnQuestGet()
        {
            // TODO:
        }
    }
}
