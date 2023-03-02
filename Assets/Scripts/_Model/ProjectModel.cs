using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Project;
using Zenject;
using System.Linq;

namespace Model
{
    public class ProjectModel 
    {
        private readonly SignalBus _signalBus;
      
        private List<IModel> _projectModels;
       
        private ProjectSaveData _projectData;

        public ProjectModel(SignalBus signalBus)
        {
            _signalBus = signalBus;

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
            if (_projectModels.Any(modelItem => modelItem.Id == model.Id)) return;

            _projectModels.Add(model);
        }

        public void RemoveModel(IModel model)
        {
            var modelRemove = _projectModels.FirstOrDefault(modelItem => modelItem.Id == model.Id);

            _projectModels.Remove(modelRemove);
        }

        public void RemoveAllModels()
        {
            _projectModels?.Clear();
        }

        public string SerializeProject(ProjectModel projectModel)
        {
            return JsonConvert.SerializeObject(projectModel);
        }

        public ProjectModel DesirializeProject(string projectModel)
        {
            return JsonConvert.DeserializeObject<ProjectModel>(projectModel);
        }

        public ProjectModel GetCurrentModel()
        {
            return this;
        }
    }
}
