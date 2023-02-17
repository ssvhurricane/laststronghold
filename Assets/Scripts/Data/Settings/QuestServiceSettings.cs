using Config;
using System;

namespace Data.Settings
{
    [Serializable]
    public class Flow
    {
        public int Id;
        public string Description;
        public string Threads;
        public int FlowExecutionMode;
        public int FlowRewardMode;
    }

    [Serializable]
    public class QuestServiceSettings :  IRegistryData
    {
        public string Id;

        public Flow[] Flows;

        string IRegistryData.Id => Id;
    }
}