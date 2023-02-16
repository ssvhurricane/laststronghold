using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestCollectController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestCollectController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestCollectEvent>(signal => OnQuestCollect());
        }

        private void OnQuestCollect()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_collect_object]");
        }
    }
}
