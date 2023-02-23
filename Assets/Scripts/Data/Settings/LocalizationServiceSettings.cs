using System;
using Config;

namespace Data.Settings
{  
    [Serializable]
    public class LocalizationItem
    {
        public string Key;

        public string RU;
        public string EN;
            /*
            public string DE;
            public string ES;
            public string FR;
            public string IT;
            public string BR;
            public string PT;
            public string DA;
            public string NO;
            public string NL;
            public string SE;
            public string JA;
            public string KO;
            public string TW;
            */
    }

    [Serializable]
    public class LocalizationServiceSettings : IRegistryData
    {
         public string Id;

         public LocalizationItem localizationItem;

         string IRegistryData.Id => Id;
    }
}
