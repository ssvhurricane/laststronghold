using Newtonsoft.Json;
using UniRx;
using Zenject;
using Data;
using System.Collections.Generic;
using Data.Settings;
using UnityEngine;
using Services.Area;
using Services.RayCast;

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
        private readonly RayCastService _rayCastService;

        public ReceiverModel(SignalBus signalBus,
                         RayCastService rayCastService)
        {
            _signalBus = signalBus;

            _rayCastService = rayCastService;

            if( _rayCastService != null
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
          
            _receiverSaveData.Value = new ReceiverSaveData()
            {
                Id = 0,

                // TODO:
            };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
            var innerSaveData = (ReceiverSaveData) saveData;

            _receiverSaveData.Value = new ReceiverSaveData()
            {
                Id = 0,

               // TODO:
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
