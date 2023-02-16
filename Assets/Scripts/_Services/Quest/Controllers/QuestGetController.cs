using System;
using Signals;
using Zenject;

namespace Services.Quest
{
    public class QuestGetController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestGetController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestGetEvent>(signal => OnQuestGet());
        }

        private void OnQuestGet()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_get_object]");
        }
    }
}
