using Data.Settings;
using Model;
using Zenject;
using UnityEngine;
using UniRx;
using Newtonsoft.Json;

namespace Services.SaveData
{
    public class SaveDataService 
    {
        private readonly SignalBus _signalBus;
        private readonly ProjectModel _projectModel;
        private readonly PlayerModel _playerModel;
        private readonly CameraModel _cameraModel;
        private readonly QuestModel _questModel;

        private readonly SaveDataServiceSettings _saveDataServiceSettings;
        public SaveDataService(SignalBus signalBus,
                                ProjectModel projectModel,
                                PlayerModel playerModel,
                                CameraModel cameraModel,
                                QuestModel questModel,
                                SaveDataServiceSettings saveDataServiceSettings) 
        {
            _signalBus = signalBus;

            _projectModel = projectModel;
            _playerModel = playerModel;
            _cameraModel  = cameraModel;
            _questModel = questModel;

            _saveDataServiceSettings = saveDataServiceSettings;

            // ProjectData.
            _projectModel.GetProjectSaveDataAsReactive().Subscribe(item => 
            {
                var serializeString =  SerializeModel(_projectModel);

                SaveData(_projectModel.Id, serializeString);
            });

            // PlayerData.
            _playerModel.GetAbilityContainerAsReactive().Subscribe(item => 
            {
              //  var serializeString =  SerializeModel(_playerModel);

              //  SaveData(_playerModel.Id, serializeString);

            });
             _playerModel.GetInventoryContainerAsReactive().Subscribe(item => 
            {
              //   var serializeString =  SerializeModel(_playerModel);

                // SaveData(_playerModel.Id, serializeString);
            });

            // CameraData.
            _cameraModel.GetAbilityContainerAsReactive().Subscribe(item => 
            {
             //   var serializeString =  SerializeModel(_cameraModel);

              //  SaveData(_cameraModel.Id, serializeString);
            });

            // QuestsData.
            _questModel.GetPlayerQuestContainerAsReactive().Subscribe(item => 
            {
              //   var serializeString =  SerializeModel(_questModel);

               // SaveData(_projectModel.Id, serializeString);
            });
        }

        public string SerializeModel(IModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public IModel DesirializeModel<TParam>(string model) where TParam : IModel
        {
            return JsonConvert.DeserializeObject<TParam>(model);
        }

        public void SaveData(string id, string projectData)
        {
           switch(_saveDataServiceSettings.SaveDataType)
           {
                case SaveDataType.PlayerPrefs:
                {
                    PlayerPrefs.SetString(id, projectData);

                    break;
                }
                case SaveDataType.Custom: 
                {
                    // TODO:
                    break;
                }
           }  
        }

        public string GetData(string id)
        {
            return PlayerPrefs.GetString(id);
        }

        public void ClearData(string id)
        {
            PlayerPrefs.DeleteKey(id);
        }

        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}