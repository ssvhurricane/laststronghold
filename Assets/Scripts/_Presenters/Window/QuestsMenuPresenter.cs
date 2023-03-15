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
        private readonly ProjectServiceSettings _projectServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private QuestMenuView _questMenuView;

        private readonly QuestModel _questModel;

        private readonly ProjectModel _projectModel;

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
                            ProjectServiceSettings projectServiceSettings,
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            QuestModel questModel,
                            ProjectModel projectModel)
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _factoryService = factoryService;
            _holderService = holderService;
            _questService = questService;
            _poolService = poolService;
            _projectService = projectService;
            _projectServiceSettings = projectServiceSettings;
            _questServiceSettings = questServiceSettings;
            _questsSettings = questsSettings;
            _questModel = questModel;
            _projectModel = projectModel;

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
            var projectChapterDatas = _projectServiceSettings.ChapterDatas;

            if (projectChapterDatas != null 
                    && !_poolService.GetPoolDatas()
                                    .Any(data => data.Name == PoolServiceConstants.QuestMenuFlowContainerViewPool))
            {
                foreach(var projectChapterData in projectChapterDatas)
                {
                                                     
                   _poolService.InitPool(new PoolData()
                    {
                        Id = PoolServiceConstants.QuestMenuFlowContainerViewPool,

                        Name = PoolServiceConstants.QuestMenuFlowContainerViewPool,

                        InitialSize = projectChapterDatas.Count(),

                         MaxSize = projectChapterDatas.Count()
                    });
                        
                    var questMenuFlowContainerView = (QuestMenuFlowContainerView)_poolService.Spawn<QuestMenuFlowContainerView>(_questMenuView.GetLeftContainer().transform, 
                                                                                                                    PoolServiceConstants.QuestMenuFlowContainerViewPool);
                    questMenuFlowContainerView.UpdateView(new QuestMenuFlowContainerViewArgs()
                    {
                        Id = projectChapterData.Id.ToString(),

                        Name = projectChapterData.ChapterName,
                    });
                }
            }

            var projectModelDataFlows =_projectModel.GetProjectSaveData().QuestFlows;

            if(projectModelDataFlows != null 
                                    && projectModelDataFlows.Count() != 0
                                     && !_poolService.GetPoolDatas()
                                    .Any(data => data.Name == PoolServiceConstants.QuestMenuFlowViewPool))
            {

                foreach(var projectModelDataFlow in projectModelDataFlows)
                {
                    _poolService.InitPool(new PoolData()
                    {
                        Id = PoolServiceConstants.QuestMenuFlowViewPool,

                        Name = PoolServiceConstants.QuestMenuFlowViewPool,

                        InitialSize = _questServiceSettings.Flows.Count(),

                        MaxSize = _questServiceSettings.Flows.Count()
                    });

                   var questMenuFlowContainerView = _poolService
                        .GetPool()
                        .GetViewPoolItems()
                        .FirstOrDefault(item => item is QuestMenuFlowContainerView 
                                            && item.Id == _questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == projectModelDataFlow.Key).ChapterId.ToString()) as QuestMenuFlowContainerView;
                       
                 
                    var questMenuFlowView = (QuestMenuFlowView)_poolService.Spawn<QuestMenuFlowView>(questMenuFlowContainerView.GetContainer().transform, 
                                                                                                                    PoolServiceConstants.QuestMenuFlowViewPool);
                    questMenuFlowView.UpdateView(new QuestMenuFlowViewArgs()
                    {
                        Id = projectModelDataFlow.Key.ToString(),

                        Name = _questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == projectModelDataFlow.Key).Description
                    });

                }
            }
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
