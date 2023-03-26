using Constants;
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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View.Window;
using Zenject;
using Signals;
using System;

namespace Presenters.Window
{
    public class MessagePresenter
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

        private MessageMenuView _messageMenuView;
        
        public MessagePresenter(SignalBus signalBus,
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

             _signalBus.Subscribe<MessageServiceSignals.ActivateMessageItemView>(signal =>
              {
                    var data = _poolService.GetPoolDatas();
                    if (data == null && _poolService == null) return;

                    if(_poolService.GetPoolDatas().Any(data => data.Name == PoolServiceConstants.MessageMenuItemViewPool))
                    {
                        var poolMessageItemViews = _poolService.GetPool().GetViewPoolItems().Where(poolItem => poolItem as MessageMenuItemView);

                        foreach(var poolMessageItemView in poolMessageItemViews)
                        {
                          if((poolMessageItemView as MessageMenuItemView).GetMessageMenuText().text == signal.Name)
                          {
                                // TODO: set color, anim ,etc
                          }
                          else
                          {

                          }
                        } 

                        var poolMessageItemDetailViews = _poolService.GetPool().GetViewPoolItems().Where(poolItem => poolItem as MessageMenuItemDetailView);

                        foreach(var poolMessageItemDetailView in poolMessageItemDetailViews)
                        {
                          if((poolMessageItemDetailView as MessageMenuItemDetailView).GetMessageDetailText().text == signal.Name)
                                poolMessageItemDetailView.GetGameObject().SetActive(true);
                          else
                                poolMessageItemDetailView.GetGameObject().SetActive(false);
                        } 
                    }
              });
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<MessageMenuView>())
                return;

            OnDisposeAll();

            _projectService.CursorLocked(true, CursorLockMode.Confined);
            _projectService.PauseGame(); 
            
            if (_windowService.GetWindow<MessageMenuView>() != null)
                _messageMenuView = (MessageMenuView)_windowService.ShowWindow<MessageMenuView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _messageMenuView = _factoryService.Spawn<MessageMenuView>(holderTansform);
            }

            if (_messageMenuView._backToGameButton != null)
            {
                OnDispose(_messageMenuView._backToGameButton);

                _messageMenuView._backToGameButton.onClick.AddListener(() => OnPauseMenuViewButtonClick(_messageMenuView._backToGameButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                 Services.Log.LogType.Error,
                 $"{_messageMenuView._backToGameButton} = null!",
                 LogOutputLocationType.Console);
            }

             CreateMessageItems();
        }

        private void CreateMessageItems()
        {
            // TODO:
        }

        public IWindow GetView() 
        {
            return _messageMenuView;
        }

        private void OnPauseMenuViewButtonClick(int buttonId)
        {
            _projectService.CursorLocked(true, CursorLockMode.Confined);

            if (buttonId == _messageMenuView._backToGameButton.GetInstanceID())
            {
                _projectService.CursorLocked(false, CursorLockMode.Locked);

                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackToGameButtonClick Method.",
                      LogOutputLocationType.Console);
               
                _windowService.HideWindow<MessageMenuView>();
                _windowService.ShowWindow<MainHUDView>();
              
                _projectService.StartGame();
            }
        }
       
        private void OnDisposeAll()
        {
            _messageMenuView?._backToGameButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}