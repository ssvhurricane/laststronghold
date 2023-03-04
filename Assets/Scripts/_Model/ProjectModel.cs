using Newtonsoft.Json;
using Services.Project;
using Zenject;
using UniRx;
using Data.Settings;

namespace Model
{
    public class ProjectModel : IModel
    { 
        [JsonProperty]
        public string Id { get; set; } = "ProjectModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        [JsonProperty]
        private ReactiveProperty<ProjectSaveData> _projectData;

        [JsonIgnore]
        private readonly SignalBus _signalBus;

        [JsonIgnore]
        private readonly ProjectServiceSettings _projectServiceSettings;
        
        public ProjectModel(SignalBus signalBus,
                            ProjectServiceSettings projectServiceSettings)
        {
            _signalBus = signalBus;

            _projectServiceSettings = projectServiceSettings;
           
            Initialize(); // TODO:
        }

        private void Initialize()
        {
             _projectData = new ReactiveProperty<ProjectSaveData>();
            _projectData.Value = new ProjectSaveData();

            _projectData.Value.Id = 1 ;//int.Parse(_projectServiceSettings.Id);
            _projectData.Value.QuestFlowId = _projectServiceSettings.QuestStartFlowId;

            _projectData.Value.GameSettingsSaveData = new GameSettingsSaveData();
            _projectData.Value.GameSettingsSaveData.ChoosenLanguage = _projectServiceSettings.GameSettingsData.ChoosenLanguage;
            _projectData.Value.GameSettingsSaveData.Audio = _projectServiceSettings.GameSettingsData.Audio;
            _projectData.Value.GameSettingsSaveData.LookSensitivity = _projectServiceSettings.GameSettingsData.LookSensitivity;
            _projectData.Value.GameSettingsSaveData.FrameRateCount = _projectServiceSettings.GameSettingsData.FrameRateCount;
            _projectData.Value.GameSettingsSaveData.Shadows = _projectServiceSettings.GameSettingsData.Shadows;
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
