using Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Settings
{
    [Serializable]
    public class Flow
    {
        public int Id;
        public string Description;
        public string Threads;
        public IReadOnlyCollection<int> ParseThreads { get; private set; }

        public void Parse()
        {
          ParseThreads =  Threads.Trim().ToCharArray().Where(item => item != ',')
                                   .Select(c => Convert.ToInt32(c.ToString())).ToArray();
        }
    }

    [Serializable]
    public class QuestServiceSettings :  IRegistryData
    {
        public string Id;

        public Flow[] Flows;

        string IRegistryData.Id => Id;
    }
}