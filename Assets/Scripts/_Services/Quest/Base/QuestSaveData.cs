using System;
using System.Collections.Generic;
using Data;

namespace Services.Quest
{
    [Serializable]
    public class QuestItemData
    {
        public int Id { get; set; }

        public int ThreadId { get; set; }
        public int CurrentValue { get; set; }
        public QuestState QuestState;
    }
    
    [Serializable]
    public class QuestSaveData : ISaveData
    {
        public int Id { get; set; }
       
        public List<QuestItemData> QuestItemDatas;
        public QuestSaveData(){}
    }
}