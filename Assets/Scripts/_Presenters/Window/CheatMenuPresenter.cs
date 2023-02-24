using Services.Anchor;
using Services.Camera;
using Services.Cheat;
using Services.Essence;
using Services.Factory;
using Services.Item;
using Services.Log;
using Services.Pool;
using Services.Project;
using Services.Scene;
using Services.Window;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View.Window;
using Zenject;

namespace Presenters.Window
{
    public class CheatMenuPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;
        private readonly ISceneService _sceneService;
        private readonly EssenceService _essenceService;
        private readonly CameraService _cameraService;
        private readonly PoolService _poolService;
        private readonly ItemService _itemService; 
        private readonly CheatService _cheatService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;
        private readonly ProjectService _projectService;

        private readonly GameSettingsPresenter _gameSettingsPresenter;

        private CheatSettingsView _cheatSettingsView;
      
        public CheatMenuPresenter(SignalBus signalBus,
            LogService logService,
            ISceneService sceneService,
            IWindowService windowService,
            EssenceService essenceService,
            CameraService cameraService,
            PoolService poolService,
            ItemService itemService, 
            FactoryService factoryService,
            HolderService holderService,
            ProjectService projectService,
            CheatService cheatService,
            GameSettingsPresenter gameSettingsPresenter
            )
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _sceneService = sceneService;
            _essenceService = essenceService;
            _cameraService = cameraService;
            _poolService = poolService;
            _itemService = itemService;

            _factoryService = factoryService;
            _holderService = holderService;
            _projectService = projectService;

            _cheatService = cheatService;

           _gameSettingsPresenter = gameSettingsPresenter;
          
            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
          
        }
        public void ShowView()
        {
            if (_windowService.IsWindowShowing<CheatSettingsView>())
                return;

            OnDisposeAll();

            _projectService.CursorLocked(true, CursorLockMode.Confined);
            _projectService.PauseGame(); 
            
            if (_windowService.GetWindow<CheatSettingsView>() != null)
                _cheatSettingsView = (CheatSettingsView)_windowService.ShowWindow<CheatSettingsView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _cheatSettingsView = _factoryService.Spawn<CheatSettingsView>(holderTansform);
            }

            if (_cheatSettingsView._backToGameButton != null)
            {
                OnDispose(_cheatSettingsView._backToGameButton);

                _cheatSettingsView._backToGameButton.onClick.AddListener(() => OnPauseMenuViewButtonClick(_cheatSettingsView._backToGameButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                 Services.Log.LogType.Error,
                 $"{_cheatSettingsView._backToGameButton} = null!",
                 LogOutputLocationType.Console);
            }

            // TODO:
            _cheatService.CheatItemControlProcessing();
        }

        public IWindow GetView() 
        {
            return _cheatSettingsView;
        }

        private void OnPauseMenuViewButtonClick(int buttonId)
        {
            _projectService.CursorLocked(true, CursorLockMode.Confined);

            if (buttonId == _cheatSettingsView._backToGameButton.GetInstanceID())
            {
                _projectService.CursorLocked(false, CursorLockMode.Locked);

                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackToGameButtonClick Method.",
                      LogOutputLocationType.Console);
               
                _windowService.HideWindow<CheatSettingsView>();
                _windowService.ShowWindow<MainHUDView>();
              
                _projectService.StartGame();
            }
        }
       
        private void OnDisposeAll()
        {
            _cheatSettingsView?._backToGameButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}