using Data.Settings;
using Model;
using Zenject;
using UnityEngine;

namespace Services.SaveData
{
    public class SaveDataService 
    {
        private readonly SignalBus _signalBus;
        private readonly ProjectModel _projectModel;

        private readonly SaveDataServiceSettings _saveDataServiceSettings;
        public SaveDataService(SignalBus signalBus,
                                ProjectModel projectModel,
                                SaveDataServiceSettings saveDataServiceSettings) 
        {
            _signalBus = signalBus;

            _projectModel = projectModel;

            _saveDataServiceSettings = saveDataServiceSettings;
        }

        public void SaveData(string projectData)
        {
           switch(_saveDataServiceSettings.SaveDataType)
           {
                case SaveDataType.PlayerPrefs:
                {
                    PlayerPrefs.SetString(_saveDataServiceSettings.Id, projectData);

                    break;
                }
                case SaveDataType.Custom: 
                {
                    // TODO:
                    break;
                }
           }  
        }

        public string GetData()
        {
            return PlayerPrefs.GetString(_saveDataServiceSettings.Id);
        }

        public void ClearData()
        {
            PlayerPrefs.DeleteKey(_saveDataServiceSettings.Id);
        }
    }
}