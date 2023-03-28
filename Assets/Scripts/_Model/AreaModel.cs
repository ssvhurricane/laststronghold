using Newtonsoft.Json;
using UniRx;
using Zenject;
using Data;
using System.Collections.Generic;
using Data.Settings;
using UnityEngine;
using Services.Area;

namespace Model
{
public class AreaModel : IModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "AreaModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<AreaSaveData> _areaSaveData = new ReactiveProperty<AreaSaveData>();

        [JsonIgnore]
        private readonly SignalBus _signalBus;
        
        [JsonIgnore]
        private readonly AreaServiceSettings[] _areaServiceSettings;

        public AreaModel(SignalBus signalBus,
                        AreaServiceSettings[] areaServiceSettings)
        {
            _signalBus = signalBus;

            _areaServiceSettings = areaServiceSettings;

            if( _areaServiceSettings!= null
                 && string.IsNullOrEmpty(PlayerPrefs.GetString(Id))) InitializeDafaultModelData();
        } 

        public string SerializeModel(IModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public IModel DesirializeModel<TParam>(string model) where TParam : IModel
        {
            return JsonConvert.DeserializeObject<TParam>(model);
        }

        public void InitializeDafaultModelData()
        { 
            var prepareAreaSaveData = new List<AreaItemData>();

            foreach(var areaSettings in _areaServiceSettings)
            {
                prepareAreaSaveData.Add(new AreaItemData()
                {
                    Id = int.Parse(areaSettings.Id),

                    Description = areaSettings.Area.Description,

                    MinLevel = areaSettings.Area.MinLevel,

                    MaxLevel = areaSettings.Area.MaxLevel,

                    CurHealthPoint = areaSettings.Area.CurHealthPoint,

                    MaxHealthPoint = areaSettings.Area.MaxHealthPoint,

                    IsInteractive = areaSettings.Area.IsInteractive,

                    StatusType = areaSettings.Area.StatusType,

                    StageType = areaSettings.Area.StageType,

                    AreaType = areaSettings.Area.AreaType
                });
             }

            _areaSaveData.Value = new AreaSaveData()
            {
                Id = 0,

                AreaItemDatas = prepareAreaSaveData
            };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
            var innerSaveData = (AreaSaveData) saveData;

            var prepareAreaSaveData = new List<AreaItemData>();

            foreach(var areaItemData in innerSaveData.AreaItemDatas)
            {
                prepareAreaSaveData.Add(new AreaItemData()
                {
                    Id = areaItemData.Id,

                   Description = areaItemData.Description,

                    MinLevel = areaItemData.MinLevel,

                    MaxLevel = areaItemData.MaxLevel,

                    CurHealthPoint = areaItemData.CurHealthPoint,

                    MaxHealthPoint = areaItemData.MaxHealthPoint,

                    IsInteractive = areaItemData.IsInteractive,

                    StatusType = areaItemData.StatusType,

                    StageType = areaItemData.StageType,
                    
                    AreaType = areaItemData.AreaType
                });
             }

            _areaSaveData.Value = new AreaSaveData()
            {
                Id = 0,

               AreaItemDatas = prepareAreaSaveData
            };
        }

        public ReactiveProperty<AreaSaveData> GetAreaSaveDataAsReactive()
        {
            return _areaSaveData;
        } 

        public AreaSaveData GetAreaSaveData()
        {
            return _areaSaveData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
