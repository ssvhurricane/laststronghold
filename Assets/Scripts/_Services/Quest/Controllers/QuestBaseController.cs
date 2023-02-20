using System;

namespace Services.Quest
{
    public class QuestBaseController : QuestBase
    { 
        private Action<QuestBase> _actionQuestProgress;
        protected string _param;
        public override void Configurate(Data.Settings.Quest quest)
        {
            base.Configurate(quest);

            Data.Id = quest.Id;

            Data.CurProgress = 0;

            Data.NeedProgress = quest.Amount;

            Data.QuestConditionType = quest.QuestConditionType;
            
            _param = quest.Param;

            _value = quest.Value;

            _objectType = _value;
        }
        public override void Activate(Action<QuestBase> action = null)
        {
           _actionQuestProgress = action;

            QuestState = QuestState.Active;
        }

        public override void Deactivate()
        {
            QuestState = QuestState.Inactive;
        }

        public override string GetDescription(string hexColor = "", string localizationId = "")
        {
            /*
            var count = string.IsNullOrEmpty(hexColor)
                ? Data.NeedProgress.ToString()
                : $"<color=#{hexColor}>{Data.NeedProgress}</color>";

            var formatString = string.Format(LocalizationManager.Instance[localizationId], count,
                ActorsHelpers.GetLocalizedName(_objectType));

            var level = ActorsHelpers.GetActorLevelString(_objectType, false);

            if (!string.IsNullOrEmpty(level)) formatString += $" ({level})";

            return formatString;*/
            return null;
        }

        public override QuestSaveDataBase GetSaveData()
        {
             return new QuestCountSaveData { QuestId = Data.Id, QuestState = QuestState, Count = Data.CurProgress };
        }

        public override void Load(QuestSaveDataBase questSaveData)
        {
           var saveData = (QuestCountSaveData)questSaveData;

            Data.CurProgress = saveData.Count;
        }

          protected void UpdateProgress()
        {
            if (Data.CurProgress >= Data.NeedProgress) QuestState = QuestState.Complete;

            _actionQuestProgress?.Invoke(this);
        }
    }
}