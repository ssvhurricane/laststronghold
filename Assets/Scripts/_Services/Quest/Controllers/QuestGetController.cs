using System;

namespace Services.Quest
{
    public class QuestGetController : QuestBaseController
    {
        public QuestGetController()
        {
            // TODO:
        }

        public override void Activate(Action<QuestBase> action = null)
        {
            base.Activate(action);

            // TODO:
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            return base.GetDescription(hexColor, "[quest_get_object]");
        }
    }
}
