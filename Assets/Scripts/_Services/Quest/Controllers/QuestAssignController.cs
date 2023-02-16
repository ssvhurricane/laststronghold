using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestAssignController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestAssignController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestAssignEvent>(signal => OnQuestAssign());
        }

        private void OnQuestAssign()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_assign_object]");
        }
    }
}
