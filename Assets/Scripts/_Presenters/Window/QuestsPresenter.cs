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

namespace Presenters
{
    public class QuestsPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly IWindowService _windowService;

        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;
        private IView _questView;

        private readonly QuestModel _questModel;
        public QuestsPresenter(SignalBus signalBus, 
                            LogService logService, 
                            IWindowService windowService,
                            FactoryService factoryService,
                            HolderService holderService,
                            QuestModel questModel)
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;
            _factoryService = factoryService;
            _holderService = holderService;
            _questModel = questModel;
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
        }
    }
}
