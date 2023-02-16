using Config;
using System;

namespace Data.Settings
{  
    [Serializable]
    public class QuestsSettings : IRegistryData
    {  
        public string Id;

         string IRegistryData.Id => Id;
    }
}
