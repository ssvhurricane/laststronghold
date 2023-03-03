using Newtonsoft.Json;
using Services.Project;
using Zenject;
using UniRx;

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
        
        public ProjectModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
           
            Initialize();
        }

        private void Initialize()
        {
             _projectData = new ReactiveProperty<ProjectSaveData>();
            _projectData.Value = new ProjectSaveData();

            _projectData.Value.Id = 0;
            _projectData.Value.QuestFlowId = 1;
            _projectData.Value.GameSettingsSaveData = new GameSettingsSaveData();
            _projectData.Value.GameSettingsSaveData.ChoosenLanguage = Services.Localization.Language.RU;
        }
        public ReactiveProperty<ProjectSaveData> GetProjectSaveDataAsReactive()
        {
            return _projectData;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
