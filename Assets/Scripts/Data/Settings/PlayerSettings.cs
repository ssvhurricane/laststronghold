using Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class PlayerSettings : IRegistryData
    {
        public string Id;
        public string Name;

        public List<string> Abilities;

        public List<string> Items;
        string IRegistryData.Id => Id;
    }
}
