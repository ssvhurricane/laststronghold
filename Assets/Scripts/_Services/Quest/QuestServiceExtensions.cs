
using System;
using UnityEngine;

namespace Services.Quest
{
    public static class QuestServiceExtensions 
    {
        public static Type GetQuestTypeByCondition(QuestConditionType questConditionType)
        {
            switch(questConditionType)
            {
                 case QuestConditionType.Activate: return typeof(QuestActivateController);
                 case QuestConditionType.Assign: return typeof(QuestAssignController);
                 case QuestConditionType.Build: return typeof(QuestBuildController);
                 case QuestConditionType.Collect: return typeof(QuestCollectController);
                 case QuestConditionType.Destroy: return typeof(QuestDestroyController);
                 case QuestConditionType.Get: return typeof(QuestGetController);
                 case QuestConditionType.Kill: return typeof(QuestKillController);
                 case QuestConditionType.Upgrade: return typeof(QuestUpgradeController);
            }

              throw new Exception($"No such quest type {questConditionType}");
        }
        public static Sprite GetSpriteByCondition(QuestConditionType questCond)
        {
          // return Storage.UIStorage.Instance.GetStorageData(questCond);
           return null;
        }
    }
}