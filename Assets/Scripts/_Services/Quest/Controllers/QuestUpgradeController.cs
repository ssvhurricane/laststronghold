using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestUpgradeController : QuestBaseController
    {
        private readonly SignalBus _signalBus;
        public QuestUpgradeController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestUpgradeEvent>(signal => OnQuestUpgrade());
        }

        private void OnQuestUpgrade()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_upgrade_object]");
        }
    }
}
