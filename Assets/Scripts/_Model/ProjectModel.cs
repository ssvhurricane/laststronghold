using System.Collections.Generic;
using Services.Project;
using Services.SaveData;
using Zenject;

namespace Model
{
    public class ProjectModel 
    {
        private readonly SignalBus _signalBus;
        private readonly SaveDataService _saveDataService;
        
        private List<IModel> _projectModels;
       
        private ProjectSaveData _projectData;

        public ProjectModel(SignalBus signalBus, SaveDataService saveDataService)
        {
            _signalBus = signalBus;

            _saveDataService = saveDataService;

            _projectModels = new List<IModel>();

            // TODO:
            _projectData = new ProjectSaveData();
            _projectData.Id = 0;
            _projectData.QuestFlowId = 1;
            _projectData.GameSettingsSaveData = new GameSettingsSaveData();
            _projectData.GameSettingsSaveData.ChoosenLanguage = Services.Localization.Language.RU;
        }

        public ProjectSaveData GetProjectSaveData()
        {
            return _projectData;
        }

        public void AddModel(IModel model)
        {
            // TODO:
        }

        public void RemoveModel(IModel model)
        {
            // TODO:
        }

        public void RemoveAllModels()
        {
            // TODO:
        }

        public void SerializeProject()
        {
            // TODO:
        }

        public void DesirializeProject()
        {
            // TODO:
        }

    }
}
