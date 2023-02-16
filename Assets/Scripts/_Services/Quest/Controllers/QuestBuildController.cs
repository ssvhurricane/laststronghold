using System;
using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestBuildController :QuestBaseController
    {       
        private readonly SignalBus _signalBus;
        public QuestBuildController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            _signalBus.Subscribe<QuestServiceSignals.OnQuestBuildEvent>(signal => OnQuestBuild());
        }

        private void OnQuestBuild()
        {
            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_build_object]");
        }
    
    }
}
