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
        public int ThreadId;
        public string Description;
        public int NeedValue;
        public Sprite Icon;
        public QuestConditionType QuestConditionType;

        public string Param;

        public string Value;
    }

    [Serializable]
    public class QuestsSettings : IRegistryData
    {  
        public string Id;

        public Quest Quest;

         string IRegistryData.Id => Id;
    }
}
