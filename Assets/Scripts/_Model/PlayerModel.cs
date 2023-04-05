using System.Collections.Generic;
using Data;
using Data.Settings;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace Model
{
    public class PlayerModel : ILiveModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "PlayerModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }
     
         //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<PlayerSaveData> _playerSaveData = new ReactiveProperty<PlayerSaveData>();

        [JsonIgnore]
        private readonly PlayerSettings _settings;

        public PlayerModel(PlayerSettings settings)
        {
            _settings = settings;

            if(settings != null
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
           _playerSaveData.Value = new PlayerSaveData
           {
                Id = int.Parse(_settings.Id),

                Abilities = _settings.Abilities,

                Items =  _settings.Items

           };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
             var innerSaveData = (PlayerSaveData) saveData;
             

             _playerSaveData.Value = new PlayerSaveData
           {
                Id = innerSaveData.Id,

                Abilities = innerSaveData.Abilities,

                Items = innerSaveData.Items
           };
        }

        public ReactiveProperty<PlayerSaveData> GetPlayerSaveDataAsReactive()
        {
            return _playerSaveData;
        } 

        public PlayerSaveData GetPlayerSaveData()
        {
            return _playerSaveData.Value;
        }

        public List<string> GetAbilities()
        {
            return _playerSaveData.Value.Abilities;
        }

        public List<string> GetItems()
        {
            return _playerSaveData.Value.Items;
        }

        public void Dispose()
        {
            // TODO:
        }
    }

    public class PlayerSaveData: ISaveData
    {
         public int Id { get; set; }
       
         public List<string> Abilities;

         public List<string> Items;
         
         public PlayerSaveData(){}
    }
}
