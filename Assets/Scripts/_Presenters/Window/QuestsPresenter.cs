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
using System.Collections.Specialized;
using System.Collections.Generic;
using Services.Pool;
using Constants;
using Services.Project;
using UniRx;

namespace Presenters
{
    public class QuestsPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;

        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly QuestService _questService;
        private readonly PoolService _poolService;

        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private IView _questView;

        private readonly QuestModel _questModel;

        private List<QuestItemView> _questItemViews;

        private int _currentFlow = 1;
        public QuestsPresenter(SignalBus signalBus, 
                            LogService logService, 
                            IWindowService windowService,
                            FactoryService factoryService,
                            HolderService holderService,
                            QuestService questService,
                            PoolService poolService,
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
            _questServiceSettings = questServiceSettings;
            _questsSettings = questsSettings;
            _questModel = questModel;

           // _questModel.GetPlayerQuestContainer().QuestSaves.CollectionChanged += QuestCollectionChanged;

            _questItemViews = new List<QuestItemView>();

            _poolService.InitPool(PoolServiceConstants.QuestItemViewPool);
        }

        public IModel GetModel()
        {
            return _questModel;
        }

        public IView GetView()
        {
            return _questView;
        }

        public void HideView()
        {
            // TODO:
        }

        public void ShowView(GameObject prefab = null, Transform hTransform = null)
        {
             if (_windowService.IsWindowShowing<QuestsContainerView>()) return; 
            
            if (_windowService.GetWindow<QuestsContainerView>() != null)
                _questView = (QuestsContainerView)_windowService.ShowWindow<QuestsContainerView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.BaseWindow).Value;

                if (holderTansform != null)
                    _questView = _factoryService.Spawn<QuestsContainerView>(holderTansform);
            }

            // TODO:
            var questContainerView  = _questView as QuestsContainerView;

            if(questContainerView == null) return;

            questContainerView.UpdateView(new QuestsContainerViewArgs()
            {
                FlowDescriptionText = _questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == _currentFlow).Description
            });
            
            foreach(var questSaveData in _questModel.GetQuestSaveData().QuestItemDatas)
            {
              //  if(questSaveData.QuestState != QuestState.Active) continue;

                var questItemView = (QuestItemView)_poolService.Spawn<QuestItemView>(questContainerView.GetQuestContainer().transform, PoolServiceConstants.QuestItemViewPool);

                questItemView.UpdateView(new QuestItemViewArgs()
                {
                    Id = questSaveData.Id,

                    Description = _questsSettings.FirstOrDefault(quest =>quest.Quest.Id == questSaveData.Id).Quest.Description,

                    QuestState = questSaveData.QuestState,

                    CurrentValue = questSaveData.CurrentValue,

                    NeedValue = _questsSettings.FirstOrDefault(quest =>quest.Quest.Id == questSaveData.Id).Quest.NeedValue
                });
            }

          // questContainerView.AttachView(_questItemViews);
        }

        public void InitializeQuests(ReactiveProperty<ProjectSaveData> projectSaveData)
        {
            _currentFlow = projectSaveData.Value.CurrentQuestFlowId;
            
            _questService.InitializeFlow(_questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == _currentFlow));
        }

        private void QuestCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { 
            //List<QuestItemViewArgs>questItemViewArgs = null; TODO:

            switch (e.Action)
           {
                case NotifyCollectionChangedAction.Add:
                {
                    // TODO:
                    if(_questView != null)
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Add",
                                    LogOutputLocationType.Console);
                   
                        (_questView as QuestsContainerView).AttachView(_questItemViews);
                    }
                    break;
                }
                case NotifyCollectionChangedAction.Remove: 
                { 
                  
                    // TODO:
                    if(_questView != null)
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Remove",
                                    LogOutputLocationType.Console);

                        (_questView as QuestsContainerView).AttachView(_questItemViews);
                    }
                    break;
                }
                case NotifyCollectionChangedAction.Replace: 
                {
                    // TODO:
                    if(_questView != null)
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Replace",
                                    LogOutputLocationType.Console);
                                    
                        (_questView as QuestsContainerView).AttachView(_questItemViews);
                    }
                    break;
                }
            }
        }
    }
}
