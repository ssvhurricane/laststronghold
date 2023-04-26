using Newtonsoft.Json;
using UniRx;
using Zenject;
using Data;
using UnityEngine;
using Services.RayCast;
using System.Collections.Generic;
using Data.Settings;

namespace Model
{
public class ReceiverModel : IModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "ReceiverModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<ReceiverSaveData> _receiverSaveData = new ReactiveProperty<ReceiverSaveData>();

        [JsonIgnore]
        private readonly SignalBus _signalBus;

        [JsonIgnore]
        private readonly ReceiverHolderSettings[] _receiverHolderSettings;


        public ReceiverModel(SignalBus signalBus, ReceiverHolderSettings[] receiverHolderSettings)
        {
            _signalBus = signalBus;

            _receiverHolderSettings = receiverHolderSettings;
        
            if( _receiverHolderSettings != null && string.IsNullOrEmpty(PlayerPrefs.GetString(Id))) InitializeDafaultModelData();
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
            // Empty Data.
            var prepareReceiverSaveData = new List<ReceiverItemData>();

            foreach(var receiverItemData in _receiverHolderSettings)
            {
                prepareReceiverSaveData .Add(new ReceiverItemData()
                {
                    Id = receiverItemData.Id,

                    Name = receiverItemData.Receiver.Name,

                    IsEnabled = receiverItemData.Receiver.IsEnabled
                });
             }

            _receiverSaveData.Value = new ReceiverSaveData()
            {
                Id = 1,

                ReceiverItemDatas = prepareReceiverSaveData
            };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
            var innerSaveData = (ReceiverSaveData) saveData;

            var prepareReceiverSaveData = new List<ReceiverItemData>();

            foreach(var receiverItemData in innerSaveData.ReceiverItemDatas)
            {
                prepareReceiverSaveData .Add(new ReceiverItemData()
                {
                    Id = receiverItemData.Id,

                    Name = receiverItemData.Name,

                    IsEnabled = receiverItemData.IsEnabled
                });
             }

            _receiverSaveData.Value = new ReceiverSaveData()
            {
                Id = innerSaveData.Id,

                ReceiverItemDatas =  prepareReceiverSaveData
            };
        }

        public ReactiveProperty<ReceiverSaveData> GetReceiverSaveDataAsReactive()
        {
            return _receiverSaveData;
        } 

        public ReceiverSaveData GetReceiverSaveData()
        {
            return _receiverSaveData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
