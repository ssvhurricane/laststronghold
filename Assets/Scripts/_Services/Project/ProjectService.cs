using Data.Settings;
using Model;
using Services.Log;
using Services.SaveData;
using UnityEngine;
using Zenject;

namespace Services.Project
{
    public class ProjectService : ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly ProjectModel _projectModel;
        private readonly QuestModel _questModel;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly ProjectServiceSettings _projectServiceSettings;

        private readonly SaveDataService _saveDataService;

        private ProjectState _projectState;
        public ProjectService(SignalBus signalBus,
            LogService logService,
            QuestModel questModel,
            ProjectModel projectModel,
            QuestServiceSettings questServiceSettings,
            ProjectServiceSettings projectServiceSettings,
            SaveDataService saveDataService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _questModel = questModel;
            _projectModel = projectModel;

            _questServiceSettings = questServiceSettings;

            _projectServiceSettings = projectServiceSettings;

            _saveDataService = saveDataService;

            _projectState = ProjectState.Stop;
        }

        public void LoadSaveData()
        {
            var projectSaveModelData = _saveDataService.GetData(_projectModel.Id);

            if(!string.IsNullOrEmpty(projectSaveModelData))
            {
               var pModel = (ProjectModel)_projectModel.DesirializeModel<ProjectModel>(projectSaveModelData);

              _projectModel.UpdateModelData(pModel.GetProjectSaveData());
            }
        }

        public ProjectType GetProjectType() 
        {
            return _projectServiceSettings.ProjectType;
        }

        public ProjectState GetProjectState() 
        {
            return _projectState;
        }

        public void SetProjectState(ProjectState projectState) 
        {
            _projectState = projectState;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            SetProjectState(ProjectState.Start);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            SetProjectState(ProjectState.Pause);
        }

        public void StopGame()
        {
            Time.timeScale = 0;
            SetProjectState(ProjectState.Stop);
        }

        public void CursorLocked(bool isLocked, CursorLockMode cursorLockMode) 
        {
            Cursor.visible = isLocked;
            Cursor.lockState = cursorLockMode;
        }

        private void UpdateGameFlow()
        {
            // TODO: need increment quest flow
        }

        public void Tick()
        {
            if(GetProjectState() == ProjectState.Start)
            {
                UpdateGameFlow();
            }
        }
    }
}