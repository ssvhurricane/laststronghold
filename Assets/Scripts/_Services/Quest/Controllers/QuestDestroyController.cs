using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestDestroyController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestDestroyController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestDestroyEvent>(signal => OnQuestDestroy());
        }

        private void OnQuestDestroy()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_destroy_object]");
        }
    }
}
