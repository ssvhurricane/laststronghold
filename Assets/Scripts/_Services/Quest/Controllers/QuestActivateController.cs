
using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestActivateController : QuestBaseController
    {
            private readonly SignalBus _signalBus;
            public QuestActivateController(SignalBus signalBus)
            {
                _signalBus = signalBus;
            }

            public override void Activate(Action<QuestBase> action = null)
            {
                base.Activate(action);

                _signalBus.Subscribe<QuestServiceSignals.OnQuestActivateEvent>(signal => OnQuestActivate());
            }

            private void OnQuestActivate()
            {
                // TODO:
            }

            public override string GetDescription(string hexColor = "", string localizationId = "")
            {
                return base.GetDescription(hexColor, "[quest_activate_object]");
            }
    }
}
