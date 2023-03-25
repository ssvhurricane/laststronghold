using System.Linq;
using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Window;
using UnityEngine;
using View.Window;
using Zenject;
using Signals;
using Services.Tutorial;
using Services.Project;
using UnityEngine.UI;
using Data.Settings;

namespace Presenters.Window
{
    public class TutorialPresenter 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly TutorialService _tutorialService;

        private readonly ProjectService _projectService;

        private TutorialView _tutorialView;

        private Tutorial _curTutorial;

        public TutorialPresenter(SignalBus signalBus,
                                 LogService logService,
                                 IWindowService windowService,
                                 FactoryService factoryService,
                                 HolderService holderService,
                                 TutorialService tutorialService,
                                ProjectService projectService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _windowService = windowService;

            _factoryService = factoryService;

            _holderService = holderService;

            _tutorialService = tutorialService;

            _projectService = projectService;

            _signalBus.Subscribe<TutorialServiceSignals.Show>(signal => OnTutorialShow(signal.Id));
        }

        private void OnTutorialShow(int id)
        {
            _curTutorial = _tutorialService.GetCurrentTutorialData(id);

            ShowView();
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<TutorialView>()) return;

            // TODO::
            _windowService.HideWindow<CheatSettingsView>();

            OnDisposeAll();

            _projectService.CursorLocked(true, CursorLockMode.Confined);
            _projectService.PauseGame(); 

            if (_windowService.GetWindow<TutorialView>() != null)
                _tutorialView = (TutorialView)_windowService.ShowWindow<TutorialView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                     _tutorialView  = _factoryService.Spawn<TutorialView>(holderTansform);
            }

            if( _tutorialView != null)
                _tutorialView.UpdateView(new TutorialViewArgs()
                {
                    Tutorial = _curTutorial
                }); 
            
            if (_tutorialView.BackToGameButton != null)
            {
                OnDispose(_tutorialView.BackToGameButton);

                _tutorialView.BackToGameButton.onClick.AddListener(() => OnPauseMenuViewButtonClick(_tutorialView.BackToGameButton.GetInstanceID()));
            }
            /*
            else
            {
                _logService.ShowLog(GetType().Name,
                 Services.Log.LogType.Error,
                 $"{_tutorialView.BackToGameButton} = null!",
                 LogOutputLocationType.Console);
            }*/
        }

        public IWindow GetView() 
        {
            return _tutorialView;
        } 
        private void OnPauseMenuViewButtonClick(int buttonId)
        {
            _projectService.CursorLocked(true, CursorLockMode.Confined);

            if (buttonId == _tutorialView.BackToGameButton.GetInstanceID())
            {
                _projectService.CursorLocked(false, CursorLockMode.Locked);
/*
                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackToGameButtonClick Method.",
                      LogOutputLocationType.Console);*/
               
               // TODO: ref win service
                _windowService.HideWindow<TutorialView>();
              
                _windowService.ShowWindow<MainHUDView>();
              
                _projectService.StartGame();
            }
        }

         private void OnDisposeAll()
        {
            _tutorialView?.BackToGameButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}
