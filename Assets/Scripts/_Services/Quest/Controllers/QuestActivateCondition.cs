using Zenject;
using Signals;

namespace Services.Quest
{
    public class QuestActivateCondition : QuestBaseCondition
    {
            private readonly SignalBus _signalBus;
            public QuestActivateCondition(SignalBus signalBus)
            {
                _signalBus = signalBus;
            }

            public override void Activate(/*Action<QuestBase> action = null*/)
            {
                base.Activate(/*action*/);

                _signalBus.Subscribe<QuestServiceSignals.OnQuestActivateEvent>(signal => OnQuestActivate());
            }

            private void OnQuestActivate()
            {
                // TODO:
            }
    }
}
