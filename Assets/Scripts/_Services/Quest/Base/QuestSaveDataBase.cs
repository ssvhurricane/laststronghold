using System;

namespace Services.Quest
{
    [Serializable]
    public abstract class QuestSaveDataBase
    {
        public int QuestId;
        public QuestState QuestState;
    }

    [Serializable]
    public class QuestCountSaveData : QuestSaveDataBase
    {
        public int Count;
    }

    [Serializable]
    public class QuestTimeSaveData : QuestSaveDataBase
    {
        public float TimeLeft;
    }
}