using System;

namespace Services.Quest
{
    public class QuestBaseController : QuestBase
    {
        public override void Activate(Action<QuestBase> action = null)
        {
            throw new NotImplementedException();
        }

        public override void Deactivate()
        {
            throw new NotImplementedException();
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            throw new NotImplementedException();
        }

        public override QuestSaveDataBase GetSaveData()
        {
            throw new NotImplementedException();
        }

        public override void Load(QuestSaveDataBase questSaveData)
        {
            throw new NotImplementedException();
        }
    }
}