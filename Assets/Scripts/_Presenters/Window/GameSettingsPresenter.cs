using Model;
using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Window;
using System.Linq;
using UnityEngine;
using View.Window;
using Zenject;
using UniRx;
using Signals;

namespace Presenters.Window
{
    public class GameSettingsPresenter 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private GameSettingsView _gameSettingsView;

       // Not recommended directly model or service, better presenter.
        private readonly ProjectModel _projectModel;

        public GameSettingsPresenter(SignalBus signalBus,
            LogService logService,
            IWindowService windowService,
            FactoryService factoryService,
            HolderService holderService,
            ProjectModel projectModel) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;

            _factoryService = factoryService;
            _holderService = holderService;

            _projectModel = projectModel;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message, 
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
            
            _projectModel.GetProjectSaveDataAsReactive().Subscribe(item => 
            {
                 if( _gameSettingsView != null)
                    _gameSettingsView.UpdateView(new GameSettingsViewArgs()
                    {
                        ProjectData = _projectModel.GetProjectSaveData()
                    }); 
            });

            _signalBus.Subscribe<GameSettingsViewSignals.Apply>(signal =>
            {
                // Refresh project model(Game settings).
                var projectModelData = _projectModel.GetProjectSaveData();

                projectModelData.GameSettingsSaveData.Audio = signal.GameSettingsViewArgs.ProjectData.GameSettingsSaveData.Audio;
                projectModelData.GameSettingsSaveData.FrameRateCount = signal.GameSettingsViewArgs.ProjectData.GameSettingsSaveData.FrameRateCount;
                projectModelData.GameSettingsSaveData.ChoosenLanguage = signal.GameSettingsViewArgs.ProjectData.GameSettingsSaveData.ChoosenLanguage;
                projectModelData.GameSettingsSaveData.LookSensitivity = signal.GameSettingsViewArgs.ProjectData.GameSettingsSaveData.LookSensitivity;
                projectModelData.GameSettingsSaveData.Shadows = signal.GameSettingsViewArgs.ProjectData.GameSettingsSaveData.Shadows;


                // TODO: fire events for changed values.
            });
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<GameSettingsView>()) return;
            
            if (_windowService.GetWindow<GameSettingsView>() != null)
                _gameSettingsView = (GameSettingsView)_windowService.ShowWindow<GameSettingsView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _gameSettingsView = _factoryService.Spawn<GameSettingsView>(holderTansform);
            }

            if( _gameSettingsView != null)
                _gameSettingsView.UpdateView(new GameSettingsViewArgs()
                {
                    ProjectData = _projectModel.GetProjectSaveData()
                }); 
        }

        public IWindow GetView() 
        {
            return _gameSettingsView;
        }
    }
}
