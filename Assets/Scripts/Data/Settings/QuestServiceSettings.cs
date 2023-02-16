using Config;
using System;

namespace Data.Settings
{
    [Serializable]
    public class QuestServiceSettings :  IRegistryData
    {
        public string Id;
        string IRegistryData.Id => Id;
    }
}