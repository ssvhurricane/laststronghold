using System;

namespace Data.Settings
{
    [Serializable]
    public class CheatMenuItem
    { 
        public int Id;

        public string Name;
    }
    [Serializable]
    public class CheatServiceSettings : IRegistryData
    {
        public string Id;

        public bool Enable;

        public CheatMenuItem[] CheatItems;

        string IRegistryData.Id => Id;
    }
}
