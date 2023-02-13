using Config;
using Services.Area;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class AreaServiceSettings : IRegistryData
    {
        public string Id;

        public int MinLevel;
        public int MaxLevel;
        public StatusType StatusType;
        public StageType StageType;
        public string Name;
        public string Description;
        public bool IsInteractive;
        public AreaType AreaType;

        public GameObject[] StagePrefabs;
      
        string IRegistryData.Id => Id;
    }
}