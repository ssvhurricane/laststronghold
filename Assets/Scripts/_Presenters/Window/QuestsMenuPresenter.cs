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
using Signals;

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

            _signalBus.Subscribe<QuestServiceSignals.ActivateQuestMenuFlowView>(signal =>
              {
                    var data = _poolService.GetPoolDatas();
                    if (data == null && _poolService == null) return;

                    if(_poolService.GetPoolDatas().Any(data => data.Name == PoolServiceConstants.QuestMenuFlowViewPool))
                    {
                        var poolQuestMenuFlowViews = _poolService.GetPool().GetViewPoolItems().Where(poolItem => poolItem as QuestMenuFlowView);

                        foreach(var poolQuestMenuFlowView in poolQuestMenuFlowViews)
                        {
                          if((poolQuestMenuFlowView as QuestMenuFlowView).GetFlowText().text == signal.Name)
                          {
                                // TODO: set color, anim ,etc
                          }
                          else
                          {

                          }
                        } 

                        var poolQuestMenuFlowDetailViews = _poolService.GetPool().GetViewPoolItems().Where(poolItem => poolItem as QuestMenuFlowDetailView);

                        foreach(var poolQuestMenuFlowDetailView in poolQuestMenuFlowDetailViews)
                        {
                          if((poolQuestMenuFlowDetailView as QuestMenuFlowDetailView).GetFlowDetailText().text == signal.Name)
                                poolQuestMenuFlowDetailView.GetGameObject().SetActive(true);
                          else
                                poolQuestMenuFlowDetailView.GetGameObject().SetActive(false);
                        } 
                    }
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
                    // Dynamic init pool.
                    _poolService.InitPool(new PoolData()
                    {
                        Id = PoolServiceConstants.QuestMenuFlowDetailViewPool,

                        Name = PoolServiceConstants.QuestMenuFlowDetailViewPool,

                        InitialSize = _questServiceSettings.Flows.Count(),

                        MaxSize = _questServiceSettings.Flows.Count()
                    });

                    var questMenuFlowDetailView = (QuestMenuFlowDetailView)_poolService.Spawn<QuestMenuFlowDetailView>(_questMenuView.GetRightcontainer().transform, 
                                                                                                                    PoolServiceConstants.QuestMenuFlowDetailViewPool);
                    
                   if(_projectModel.GetProjectSaveData().CurrentQuestFlowId == projectModelDataFlow.Key && projectModelDataFlow.Value)
                    {  
                        questMenuFlowDetailView.gameObject.SetActive(true);

                        questMenuFlowDetailView.ToggleActive(true);
                    } 
                    else
                    {
                        questMenuFlowDetailView.gameObject.SetActive(false);

                        questMenuFlowDetailView.ToggleActive(false);
                    }
                  
                    questMenuFlowDetailView.UpdateView(new QuestMenuFlowDetailViewArgs()
                    {
                        Id = projectModelDataFlow.Key.ToString(),

                        FlowName = _questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == projectModelDataFlow.Key).Description
                    });
                    
                    var curFlow = _questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == projectModelDataFlow.Key);
                  
                    curFlow.Parse();

                    var questItemDatas = _questModel.GetQuestSaveData().QuestItemDatas.Where(data => curFlow.ParseThreads
                                                                                      .Any(item => item == data.ThreadId));

                    foreach(var questSaveData in questItemDatas)
                    {
                        var questItemView = (QuestItemView)_factoryService.Spawn<QuestItemView>(questMenuFlowDetailView.GetContainer().transform);

                        questItemView.UpdateView(new QuestItemViewArgs()
                        {
                            Id = questSaveData.Id,

                            Description = _questsSettings.FirstOrDefault(quest => quest.Quest.Id == questSaveData.Id).Quest.Description
                        });
                        
                    }
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
