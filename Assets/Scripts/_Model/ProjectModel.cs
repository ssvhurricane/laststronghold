using Newtonsoft.Json;
using Services.Project;
using Zenject;
using UniRx;
using Data.Settings;
using UnityEngine;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class ProjectModel : IModel, ISerializableModel
    { 
        [JsonProperty]
        public string Id { get; set; } = "ProjectModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<ProjectSaveData> _projectData = new ReactiveProperty<ProjectSaveData>();

        [JsonIgnore]
        private readonly SignalBus _signalBus;

        [JsonIgnore]
        private readonly ProjectServiceSettings _projectServiceSettings;

        [JsonIgnore]
        private readonly QuestServiceSettings _questServiceSettings;

        public ProjectModel(SignalBus signalBus,
                            ProjectServiceSettings projectServiceSettings,
                            QuestServiceSettings questServiceSettings)
        {
            _signalBus = signalBus;

            _projectServiceSettings = projectServiceSettings; 

            _questServiceSettings = questServiceSettings;
            
            if(_projectServiceSettings != null
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
                var prepareQuestFlowsData = new Dictionary<int, bool>();

                foreach(var itemDataSettings in _questServiceSettings.Flows)
                    if(itemDataSettings.Id == _projectServiceSettings.QuestCurrentFlowId)
                        prepareQuestFlowsData.Add(itemDataSettings.Id, true);
                    else
                        prepareQuestFlowsData.Add(itemDataSettings.Id, false);
           
               _projectData.Value = new Services.Project.ProjectSaveData()
                {
                    Id = int.Parse(_projectServiceSettings.Id),
                    
                    CurrentQuestFlowId = _projectServiceSettings.QuestCurrentFlowId,

                    QuestFlows = prepareQuestFlowsData,

                    GameSettingsSaveData = new Services.Project.GameSettingsSaveData()
                    {         
                        Audio = _projectServiceSettings.GameSettingsData.Audio,

                        FrameRateCount = _projectServiceSettings.GameSettingsData.FrameRateCount,

                        ChoosenLanguage = _projectServiceSettings.GameSettingsData.ChoosenLanguage,

                        LookSensitivity = _projectServiceSettings.GameSettingsData.LookSensitivity,
                        
                        Shadows = _projectServiceSettings.GameSettingsData.Shadows
                    }
                };
        }

        public void UpdateModelData(ISaveData saveData)
        { 
             var innerSaveData = (ProjectSaveData) saveData;

            _projectData.Value = new Services.Project.ProjectSaveData()
                {
                    Id =  innerSaveData.Id,
                    
                    CurrentQuestFlowId =  innerSaveData.CurrentQuestFlowId,

                    QuestFlows = innerSaveData.QuestFlows,

                    GameSettingsSaveData = new Services.Project.GameSettingsSaveData()
                    {         
                        Audio = innerSaveData.GameSettingsSaveData.Audio,

                        FrameRateCount = innerSaveData.GameSettingsSaveData.FrameRateCount,

                        ChoosenLanguage = innerSaveData.GameSettingsSaveData.ChoosenLanguage,

                        LookSensitivity = innerSaveData.GameSettingsSaveData.LookSensitivity,
                        
                        Shadows = innerSaveData.GameSettingsSaveData.Shadows
                    }
                };
        }

        public ReactiveProperty<ProjectSaveData> GetProjectSaveDataAsReactive()
        {
            return _projectData;
        } 

        public ProjectSaveData GetProjectSaveData()
        {
            return _projectData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
