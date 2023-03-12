using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestAssignConditon : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestAssignConditon(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestAssignEvent>(signal => OnQuestAssign());
        }

        private void OnQuestAssign()
        {
            // TODO:
        }
    }
}
