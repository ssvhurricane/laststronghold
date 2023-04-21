using System;
using System.Collections.Generic;
using Data;

namespace Services.RayCast
{
    [Serializable]
    public class ReceiverItemData
    {
        public string Id;

        public string Name; 

        public bool IsEnabled;
       
    }
    public class ReceiverSaveData : ISaveData
    {
         public int Id { get; set; }
         public List<ReceiverItemData> ReceiverItemDatas;
         public ReceiverSaveData(){}
    }
}