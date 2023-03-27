using Newtonsoft.Json;
using UniRx;
using Zenject;
using Data;
using Services.Message;
using System.Collections.Generic;
using Data.Settings;
using UnityEngine;

namespace Model
{
    public class MessageModel : IModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "MessageModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<MessageSaveData> _messageSaveData = new ReactiveProperty<MessageSaveData>();

        [JsonIgnore]
        private readonly SignalBus _signalBus;
        
        [JsonIgnore]
        private readonly MessageServiceSettings[] _messageServiceSettings;
        public MessageModel(SignalBus signalBus,
                            MessageServiceSettings[] messageServiceSettings)
        {
            _signalBus = signalBus;

            _messageServiceSettings = messageServiceSettings;

            if( _messageServiceSettings!= null
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
            var prepareMessageSaveData = new List<MessageItemData>();

            foreach(var messageSettings in _messageServiceSettings)
            {
                prepareMessageSaveData.Add(new MessageItemData()
                {
                    Id = messageSettings.Message.Id,

                    Description = messageSettings.Message.Description,

                    Date = messageSettings.Message.Date,

                    MessageOwnerName = messageSettings.Message.MessageOwnerName
                });
             }

            _messageSaveData.Value = new MessageSaveData()
            {
                Id = 0,

                MessageItemDatas = prepareMessageSaveData
            };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
            var innerSaveData = (MessageSaveData) saveData;

            var prepareMessageSaveData = new List<MessageItemData>();

            foreach(var messageItemData in innerSaveData.MessageItemDatas)
            {
                prepareMessageSaveData.Add(new MessageItemData()
                {
                    Id = messageItemData.Id,

                    Description = messageItemData.Description,

                    Date = messageItemData.Date,

                    MessageOwnerName = messageItemData.MessageOwnerName,

                    IsShown = messageItemData.IsShown
                });
             }

            _messageSaveData.Value = new MessageSaveData()
            {
                Id = 0,

               MessageItemDatas = prepareMessageSaveData
            };
        }

        public ReactiveProperty<MessageSaveData> GetMessageSaveDataAsReactive()
        {
            return _messageSaveData;
        } 

        public MessageSaveData GetMessageSaveData()
        {
            return _messageSaveData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}