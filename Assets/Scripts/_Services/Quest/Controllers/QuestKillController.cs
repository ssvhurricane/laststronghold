using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestKillController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestKillController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestKillEvent>(signal => OnQuestKill());
        }

        private void OnQuestKill()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_kill_object]");
        }
    }
}