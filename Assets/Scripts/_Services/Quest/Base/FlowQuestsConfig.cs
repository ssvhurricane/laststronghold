using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Quest
{
    [Serializable]
    public class FlowQuestsConfig 
    {
        public int Id;

        [SerializeField] private string Threads;

        public IReadOnlyCollection<int> ParseThreads { get; private set; }

        public FlowExecutionMode FlowExecutionMode; // Feature Func...

        public FlowRewardMode FlowRewardMode;  // Feature Func...
    }
}