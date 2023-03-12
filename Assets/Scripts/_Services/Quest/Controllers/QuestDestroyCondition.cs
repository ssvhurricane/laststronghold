using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestDestroyCondition : QuestBaseCondition
    {
        private readonly SignalBus _signalBus;
        public QuestDestroyCondition(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Activate()
        {
            base.Activate();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestDestroyEvent>(signal => OnQuestDestroy());
        }

        private void OnQuestDestroy()
        {
            // TODO:
        }
    }
}
