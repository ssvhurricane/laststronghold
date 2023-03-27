using System;
using System.Collections.Generic;
using Data;

namespace Services.Message
{
    [Serializable]
    public class MessageItemData
    {
        public int Id { get; set; }

        public string Description;

        public string Date;

        public MessageOwnerName MessageOwnerName;

        public bool IsShown;
    }

    public class MessageSaveData : ISaveData
    {
         public int Id { get; set; }
         public List<MessageItemData> MessageItemDatas;
         public MessageSaveData(){}
    }
}
