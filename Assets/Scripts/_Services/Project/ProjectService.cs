using Constants;
using Data.Settings;
using Model;
using Services.Cheat;
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
        private readonly CheatService _cheatService;
        private readonly ProjectModel _projectModel;
        private readonly PlayerModel _playerModel;
        private readonly CameraModel _cameraModel;
        private readonly QuestModel _questModel;
        private readonly MessageModel _messageModel;
        private readonly AreaModel _areaModel;
        private readonly ReceiverModel _receiverModel;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly ProjectServiceSettings _projectServiceSettings;

        private readonly SaveDataService _saveDataService;

        private ProjectState _projectState;
        public ProjectService(SignalBus signalBus,
            LogService logService,
            CheatService cheatService,
            QuestModel questModel,
            ProjectModel projectModel,
            PlayerModel playerModel,
            CameraModel cameraModel,
            MessageModel messageModel,
            AreaModel areaModel,
            ReceiverModel receiverModel,
            QuestServiceSettings questServiceSettings,
            ProjectServiceSettings projectServiceSettings,
            SaveDataService saveDataService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _cheatService = cheatService;

            _questModel = questModel;
            _projectModel = projectModel;
            _playerModel = playerModel;
            _cameraModel = cameraModel;
            _messageModel = messageModel;
            _areaModel = areaModel;
            _receiverModel = receiverModel;

            _questServiceSettings = questServiceSettings;

            _projectServiceSettings = projectServiceSettings;

            _saveDataService = saveDataService;

            _projectState = ProjectState.Stop;

            AddCheats();
        }

        public void LoadSaveData()
        {
            // Set ProjectSaveData.
            var projectSaveModelData = _saveDataService.GetData(_projectModel.Id);

            if(!string.IsNullOrEmpty(projectSaveModelData))
            {
               var prModel = (ProjectModel)_projectModel.DesirializeModel<ProjectModel>(projectSaveModelData);

              _projectModel.UpdateModelData(prModel.GetProjectSaveData());
            }

             // Set PlayerSaveData.
            var playerSaveModelData = _saveDataService.GetData(_playerModel.Id);

            if(!string.IsNullOrEmpty(playerSaveModelData))
            {
               var plModel = (PlayerModel)_playerModel.DesirializeModel<PlayerModel>(playerSaveModelData);

              _playerModel.UpdateModelData(plModel.GetPlayerSaveData());
            }

             // Set CameraSaveData.
            var cameraSaveModelData = _saveDataService.GetData(_cameraModel.Id);

            if(!string.IsNullOrEmpty(cameraSaveModelData))
            {
               var cModel = (CameraModel)_cameraModel.DesirializeModel<CameraModel>(cameraSaveModelData);

              _cameraModel.UpdateModelData(cModel.GetCameraSaveData());
            }

            // Set QuestSaveData.
            var questSaveModelData = _saveDataService.GetData(_questModel.Id);

            if(!string.IsNullOrEmpty(questSaveModelData))
            {
                var qModel = (QuestModel) _questModel.DesirializeModel<QuestModel>(questSaveModelData);

                _questModel.UpdateModelData(qModel.GetQuestSaveData());
            }

            // Set MessageSaveData.
            var messageSaveModelData = _saveDataService.GetData(_messageModel.Id);

            if(!string.IsNullOrEmpty(messageSaveModelData))
            {
                var mModel = (MessageModel) _messageModel.DesirializeModel<MessageModel>(messageSaveModelData);

                _messageModel.UpdateModelData(mModel.GetMessageSaveData());
            }

            // Set AreaSaveData.
            var areaSaveModelData = _saveDataService.GetData(_areaModel.Id);

            if(!string.IsNullOrEmpty(areaSaveModelData))
            {
                var aModel = (AreaModel) _areaModel.DesirializeModel<AreaModel>(areaSaveModelData);

                _areaModel.UpdateModelData(aModel.GetAreaSaveData());
            }

             // Set ReceiverSaveData.
            var receiverSaveModelData = _saveDataService.GetData(_receiverModel.Id);

            if(!string.IsNullOrEmpty(receiverSaveModelData))
            {
                var rModel = (ReceiverModel) _receiverModel.DesirializeModel<ReceiverModel>(receiverSaveModelData);

                _receiverModel.UpdateModelData(rModel.GetReceiverSaveData());
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

        private void AddCheats()
        {
            _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Clear All Save Data")
            .SetButtonCallback(() =>
            {
                _saveDataService.ClearAllData();

                _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "Cheat call: Clear All Save Data",
                                LogOutputLocationType.Console);

            }), CheatServiceConstants.General);
        }
    }
}