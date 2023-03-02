using System;
using Services.SaveData;

namespace Data.Settings
{
    [Serializable]
    public class SaveDataServiceSettings :  IRegistryData
    {
        public string Id;

        public SaveDataType SaveDataType;
      
        public SaveDataMode SaveDataMode;

        string IRegistryData.Id => Id;
    }
}