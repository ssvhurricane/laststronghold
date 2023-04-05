using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Settings;
using Newtonsoft.Json;
using Services.Ability;
using UniRx;
using UnityEngine;

namespace Model
{
    public class CameraModel : ILiveModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "CameraModel";

        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<CameraSaveData> _cameraSaveData = new ReactiveProperty<CameraSaveData>();

        [JsonIgnore]
        private readonly CameraServiceSettings[] _settings;

        public CameraModel(CameraServiceSettings[] settings,
                           CameraRotateAbility cameraRotateAbility)
                         
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
            // TODO:
            var curCamSetting = _settings.FirstOrDefault(item => item.IsActive);
          
            _cameraSaveData.Value = new CameraSaveData
            { 
                Id  = int.Parse(curCamSetting.Id),

                IsActive = curCamSetting.IsActive,

                Name = curCamSetting.Name,

                CameraType = curCamSetting.CameraType,

             //   Position = curCamSetting.Position,

             //   Rotation = curCamSetting.Rotation,

                CameraFollowSmoothSpeed = curCamSetting.CameraFollowSmoothSpeed,

            //    CameraFollowOffset = curCamSetting.CameraFollowOffset,

                Abilities =  curCamSetting.Abilities,

                Items = curCamSetting.Items
            };
        }

        public void UpdateModelData(ISaveData saveData)
        {
            // TODO:
            var innerSaveData = (CameraSaveData) saveData;

            _cameraSaveData.Value = new CameraSaveData
            {
                 Id  = innerSaveData.Id,

                IsActive = innerSaveData.IsActive,

                Name = innerSaveData.Name,

                CameraType = innerSaveData.CameraType,

             //   Position = innerSaveData.Position,

            //    Rotation = innerSaveData.Rotation,

                CameraFollowSmoothSpeed = innerSaveData.CameraFollowSmoothSpeed,

              //  CameraFollowOffset = innerSaveData.CameraFollowOffset,

                Abilities = innerSaveData.Abilities,

                Items = innerSaveData.Items
            };
        }
       
       
        public ReactiveProperty<CameraSaveData> GetCameraSaveDataAsReactive()
        {
            return _cameraSaveData;
        } 

        public CameraSaveData GetCameraSaveData()
        {
            return _cameraSaveData.Value;
        }

        public List<string> GetAbilities()
        {
            return _cameraSaveData.Value.Abilities;
        }

        public List<string> GetItems()
        {
            return _cameraSaveData.Value.Items;
        } 
        
         public void Dispose()
        {
            // TODO:
        }
    }

    public class CameraSaveData : ISaveData
    {
         public int Id { get; set; }

         public bool IsActive;

         public string Name;

         public Services.Camera.CameraType CameraType;

      //   public Vector3 Position;
      //   public Vector3 Rotation;

         public float CameraFollowSmoothSpeed;

      //   public Vector3 CameraFollowOffset;
       
         public List<string> Abilities;

         public List<string> Items;
         
         public CameraSaveData(){}
    }
}
