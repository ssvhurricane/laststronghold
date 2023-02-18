using Config;
using System;
using System.Collections.Generic;

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
        public IReadOnlyCollection<int> ParseThreads { get; private set; }
    }

    [Serializable]
    public class QuestServiceSettings :  IRegistryData
    {
        public string Id;

        public Flow[] Flows;

        string IRegistryData.Id => Id;
    }
}