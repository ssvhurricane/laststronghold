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

        public void Configurate()
        {
           // TODO:
           //1. Load save data 
         
            var projectStringModel = _projectModel.SerializeProject(_projectModel);

           _saveDataService.SaveData(projectStringModel);

            _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "SerializeProject:" + projectStringModel,
                                LogOutputLocationType.Console);

           if(!string.IsNullOrEmpty(_saveDataService.GetData()))
           {
              var projectModel = _projectModel.DesirializeProject(_saveDataService.GetData());

              _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "DesirializeProject:" +  projectModel,
                                LogOutputLocationType.Console);

           }

           //2. Update Project Model
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