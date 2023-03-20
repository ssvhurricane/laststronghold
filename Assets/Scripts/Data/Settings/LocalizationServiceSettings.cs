using System;
using UnityEngine;

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
    public class LocalizationItemFromTextFile
    {
        public string Key;

        public TextAsset RU;
        public TextAsset EN;
    }

    [Serializable]
    public class LocalizationServiceSettings : IRegistryData
    {
         public string Id;

         public bool Enable;

         public LocalizationItem localizationItem;

         public LocalizationItemFromTextFile LocalizationItemFromTextFile;

         string IRegistryData.Id => Id;
    }
}
