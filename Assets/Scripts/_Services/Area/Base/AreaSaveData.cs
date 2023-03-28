using System;
using System.Collections.Generic;
using Data;

namespace Services.Area
{
    [Serializable]
    public class AreaItemData
    {
        public int Id { get; set; }

        public string Name; 
        public string Description; 
        public int MinLevel;
        public int MaxLevel;

        public float CurHealthPoint;
        public float MaxHealthPoint;
        public bool IsInteractive;
        public StatusType StatusType;
        public StageType StageType;
        public AreaType AreaType;
    }


    public class AreaSaveData : ISaveData
    {
         public int Id { get; set; }
         public List<AreaItemData> AreaItemDatas;
         public AreaSaveData(){}
    }
}