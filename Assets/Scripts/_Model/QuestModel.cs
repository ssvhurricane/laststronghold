using System.Collections.Generic;
using Data;
using Data.Settings;
using Newtonsoft.Json;
using Services.Quest;
using UniRx;
using UnityEngine;
using Zenject;

namespace Model
{
    public class QuestModel : IModel,  ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "QuestModel";

        [JsonProperty]
        public ModelType ModelType { get; set; }

        [JsonProperty]
        private ReactiveProperty<QuestSaveData> _questSaveData = new ReactiveProperty<QuestSaveData>();
 
        [JsonIgnore]
        private readonly SignalBus _signalBus;

        [JsonIgnore]
        private readonly QuestsSettings[] _questsSettings;

        public QuestModel(SignalBus signalBus,
                            QuestsSettings[] questsSettings)
        {
            _signalBus = signalBus;
            _questsSettings = questsSettings;

            if( _questsSettings!= null
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
            var prepareQuestSaveData = new List<QuestItemData>();

            foreach(var questSettings in _questsSettings)
            {
                prepareQuestSaveData.Add(new QuestItemData()
                {
                    Id = questSettings.Quest.Id,

                    CurrentValue = 0,

                    QuestState = QuestState.Inactive
                });
             }

            _questSaveData.Value = new QuestSaveData()
            {
                Id = 0,

                QuestItemDatas = prepareQuestSaveData
            };
        }

        public void UpdateModelData(ISaveData saveData)
        {
            var innerSaveData = (QuestSaveData) saveData;

            var prepareQuestSaveData = new List<QuestItemData>();

            foreach(var questItemData in innerSaveData.QuestItemDatas)
            {
                prepareQuestSaveData.Add(new QuestItemData()
                {
                    Id = questItemData.Id,

                    CurrentValue = questItemData.CurrentValue,

                    QuestState = questItemData.QuestState
                });
             }

            _questSaveData.Value = new QuestSaveData()
            {
                Id = 0,

                QuestItemDatas = prepareQuestSaveData
            };
        }

        public  ReactiveProperty<QuestSaveData> GetQuestSaveDataAsReactive()
        {
            return _questSaveData;
        }

        public QuestSaveData GetQuestSaveData()
        {
            return _questSaveData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
