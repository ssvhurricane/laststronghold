using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestBuildCondition :QuestBaseCondition
    {       
        private readonly SignalBus _signalBus;
        public QuestBuildCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestBuildEvent>(signal => OnQuestBuild());
        }

        private void OnQuestBuild()
        {
            // TODO:
        }
    }
}
