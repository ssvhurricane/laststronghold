using Services.Area;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class Area
    {  
        public string Id;
        public string Name; 
        public string Description; 
        public int MinLevel;
        public int MaxLevel;

        public float CurHealthPoint;
        public float MaxHealthPoint;
        public bool IsInteractive;
        public StatusType StatusType;
        public StageType StageType;
        public AreaType AreaType;
    }

    [Serializable]
    public class AreaServiceSettings : IRegistryData
    {
        public string Id;

        public Area Area;

        public GameObject[] StagePrefabs;
        public GameObject[] EnvironmentPrefabs;
        string IRegistryData.Id => Id;
    }
}