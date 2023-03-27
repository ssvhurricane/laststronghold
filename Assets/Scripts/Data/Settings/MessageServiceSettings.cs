using System;
using Services.Message;

namespace Data.Settings
{  
    [Serializable]
    public class Message
    {
        public int Id;

        public string Description;

        public string Date;

        public MessageOwnerName MessageOwnerName;
    }

    [Serializable]
    public class MessageServiceSettings : IRegistryData
    {
        public string Id;

        public Message Message;
            
        string IRegistryData.Id => Id;
    }
}