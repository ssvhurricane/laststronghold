using System;
using Data;

namespace Services.Quest
{
    [Serializable]
    public abstract class QuestSaveData : ISaveData
    {
        public int Id { get; set; }
        public QuestState QuestState;
    }

    [Serializable]
    public class QuestCountSaveData : QuestSaveData
    {
        public int Count;
    }

    [Serializable]
    public class QuestTimeSaveData : QuestSaveData
    {
        public float TimeLeft;
    }
}