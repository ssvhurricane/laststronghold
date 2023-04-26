using System;

namespace Data.Settings
{
    [Serializable]
    public class Receiver
    {  
        public string Id;

        public string Name; 

        public bool IsEnabled;
    }

    [Serializable]
    public class ReceiverHolderSettings : IRegistryData
    {
        public string Id;

        public Receiver Receiver;
        
        string IRegistryData.Id => Id;
    }
}