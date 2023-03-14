using Model;
using Services.Log;
using Services.Window;
using UnityEngine;
using View;
using View.Window;
using Zenject;
using Services.Factory;
using Services.Anchor;
using System.Linq;
using Services.Quest;
using Data.Settings;
using System.Collections.Generic;
using Services.Pool;
using Constants;
using Services.Project;
using UniRx;
using UnityEngine.UI;

namespace Presenters
{
    public class QuestsMenuPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;

        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly QuestService _questService;
        private readonly PoolService _poolService;
        private readonly ProjectService _projectService;

        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private QuestMenuView _questMenuView;

        private readonly QuestModel _questModel;

        private List<QuestItemView> _questItemViews;
       
        private ReactiveProperty<ProjectSaveData> _projectSaveData;

        public QuestsMenuPresenter(SignalBus signalBus, 
                            LogService logService, 
                            IWindowService windowService,
                            FactoryService factoryService,
                            HolderService holderService,
                            QuestService questService,
                            PoolService poolService,
                            ProjectService projectService,
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            QuestModel questModel)
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _factoryService = factoryService;
            _holderService = holderService;
            _questService = questService;
            _poolService = poolService;
            _projectService = projectService;
            _questServiceSettings = questServiceSettings;
            _questsSettings = questsSettings;
            _questModel = questModel;

            _questItemViews = new List<QuestItemView>();

            //_poolService.InitPool(PoolServiceConstants.QuestItemViewPool);

            _questModel.GetQuestSaveDataAsReactive().Subscribe(item => 
            {
                /*
                var data = _poolService.GetPoolDatas();

                if (data == null && _poolService == null) return;

                if(_poolService.GetPoolDatas().Any(data => data.Name == PoolServiceConstants.QuestItemViewPool))
                {
                    var poolQuestItemViews = _poolService.GetPool().GetViewPoolItems().Where(poolItem => poolItem as QuestItemView);

                    if(poolQuestItemViews != null && poolQuestItemViews.Count() != 0)
                    {
                        var questItemViews = poolQuestItemViews.Where(view => item.QuestItemDatas.Any(item => item.Id == int.Parse(view.Id)));

                        if(questItemViews != null &&  questItemViews.Count() != 0)
                                foreach(QuestItemView questItemView in questItemViews)
                                {
                                   var itemData = item.QuestItemDatas.FirstOrDefault(data => data.Id == int.Parse(questItemView.Id));

                                   questItemView.UpdateView(new QuestItemViewArgs()
                                    {
                                        Id = itemData.Id,

                                        Description = _questsSettings.FirstOrDefault(quest => quest.Quest.Id == itemData.Id).Quest.Description,

                                        QuestState = itemData.QuestState,

                                        CurrentValue = itemData.CurrentValue,

                                        NeedValue = _questsSettings.FirstOrDefault(quest => quest.Quest.Id ==itemData.Id).Quest.NeedValue
                                    });
                                }
                    }
                }*/
            });
        } 

        public void ShowView(GameObject prefab = null, Transform hTransform = null)
        {
            if (_windowService.IsWindowShowing<QuestMenuView>()) return; 

            OnDisposeAll();

            _projectService.CursorLocked(true, CursorLockMode.Confined);
            _projectService.PauseGame(); 

            if (_windowService.GetWindow<QuestMenuView>() != null)
                _questMenuView = (QuestMenuView)_windowService.ShowWindow<QuestMenuView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _questMenuView = _factoryService.Spawn<QuestMenuView>(holderTansform);
            }

            if (_questMenuView._backToGameButton != null)
            {
                OnDispose(_questMenuView._backToGameButton);

                _questMenuView._backToGameButton.onClick.AddListener(() => OnPauseMenuViewButtonClick(_questMenuView._backToGameButton.GetInstanceID()));
            }
            else
            {
                _logService.ShowLog(GetType().Name,
                 Services.Log.LogType.Error,
                 $"{_questMenuView._backToGameButton} = null!",
                 LogOutputLocationType.Console);
            }

            CreateQuestItems();
        }

        private void CreateQuestItems()
        {
            // TODO:
        }

        public IModel GetModel()
        {
            return _questModel;
        }

        public IView GetView()
        {
            return _questMenuView;
        }

        public void HideView()
        {
            _questMenuView.Hide();
        }

        private void OnPauseMenuViewButtonClick(int buttonId)
        {
            _projectService.CursorLocked(true, CursorLockMode.Confined);

            if (buttonId == _questMenuView._backToGameButton.GetInstanceID())
            {
                _projectService.CursorLocked(false, CursorLockMode.Locked);

                _logService.ShowLog(GetType().Name,
                      Services.Log.LogType.Message,
                      "Call OnBackToGameButtonClick Method.",
                      LogOutputLocationType.Console);
               
                _windowService.HideWindow<QuestMenuView>();
                _windowService.ShowWindow<MainHUDView>();
              
                _projectService.StartGame();
            }
        }

        private void OnDisposeAll()
        {
            _questMenuView?._backToGameButton?.onClick.RemoveAllListeners();
        }

        private void OnDispose(Button disposable)
        {
            disposable?.onClick.RemoveAllListeners();
        }
    }
}
