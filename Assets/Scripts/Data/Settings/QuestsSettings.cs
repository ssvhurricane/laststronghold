using Config;
using Services.Quest;
using System;
using UnityEngine;

namespace Data.Settings
{  
    [Serializable]
    public class Quest 
    {
        public int Id;
        public int ParentQuestId;
        public int ThreadId;
        public string Param;
        public string Description;
        public string Tag;
        public string Value;
        public Sprite Icon;
        public QuestConditionType QuestConditionType;
    }

    [Serializable]
    public class QuestsSettings : IRegistryData
    {  
        public string Id;

        public Quest Quest;

         string IRegistryData.Id => Id;
    }
}
