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

        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private IView _questView;

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
            
            if (_windowService.GetWindow<QuestMenuView>() != null)
                _questView = (QuestMenuView)_windowService.ShowWindow<QuestMenuView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.PopUpWindow).Value;

                if (holderTansform != null)
                    _questView = _factoryService.Spawn<QuestMenuView>(holderTansform);
            }
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
    }
}
