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

        private readonly QuestServiceSettings _questServiceSettings;
        private IView _questView;

        private readonly QuestModel _questModel;
        public QuestsPresenter(SignalBus signalBus, 
                            LogService logService, 
                            IWindowService windowService,
                            FactoryService factoryService,
                            HolderService holderService,
                            QuestService questService,
                            QuestServiceSettings questServiceSettings,
                            QuestModel questModel)
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _factoryService = factoryService;
            _holderService = holderService;
            _questService = questService;
            _questServiceSettings = questServiceSettings;
            _questModel = questModel;

            _questModel.GetPlayerQuestContainer().QuestSaves.CollectionChanged += QuestCollectionChanged;
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
            List<QuestsContainerViewArgs> questsContainerViewArgs = null;

           (_questView as QuestsContainerView).UpdateView(questsContainerViewArgs);
        }

        public void InitializeQuests()
        {
            _questService.InitializeFlow(_questServiceSettings.Flows.FirstOrDefault(flow => flow.Id == 1));
        }

        private void QuestCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { 
            List<QuestsContainerViewArgs> questsContainerViewArgs = null;

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
                   
                        (_questView as QuestsContainerView).UpdateView(questsContainerViewArgs);
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

                        (_questView as QuestsContainerView).UpdateView(questsContainerViewArgs);
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
                                    
                        (_questView as QuestsContainerView).UpdateView(questsContainerViewArgs);
                    }
                    break;
                }
            }
        }
    }
}
