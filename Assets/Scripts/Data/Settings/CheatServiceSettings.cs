using System;

namespace Data.Settings
{
    [Serializable]
    public class CheatItem
    { 
        public int Id;

        public string Name;
        public  CheatItemControl[] CheatItemControls;
    }

    [Serializable]
    public class CheatItemControl
    {
        // TODO:
        public int Id;

        public string Name;
    }

    [Serializable]
    public class CheatServiceSettings : IRegistryData
    {
        public string Id;

        public bool Enable;

        public CheatItem[] CheatItems;

        string IRegistryData.Id => Id;
    }
}
