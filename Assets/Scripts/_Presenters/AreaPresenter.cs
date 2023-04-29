using Model;
using Zenject;
using Services.Log;
using Services.Essence;
using Services.Anchor;
using Services.Pool;
using Services.Factory;
using View;
using UniRx;
using System.Linq;
using Signals;
using Constants;

namespace Presenters
{
    public class AreaPresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly AreaModel _areaModel;
        private readonly EssenceService _essenceService;
        private readonly AnchorService _anchorService;
        private readonly PoolService _poolService;
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;
       

        public AreaPresenter(SignalBus signalBus,
                            LogService logService,
                            AreaModel areaModel,
                            EssenceService essenceService,
                            AnchorService anchorService,
                            PoolService poolService,
                            FactoryService factoryService,
                            HolderService holderService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _areaModel = areaModel;

            _essenceService = essenceService;

            _anchorService = anchorService;

            _poolService = poolService;

            _factoryService = factoryService;

            _holderService = holderService;

            _signalBus.Subscribe<SceneServiceSignals.SceneLoadingCompleted>(data =>
            {
                
                if (data.Data == SceneServiceConstants.OfflineLevel1)
                {
                    ShowView();
                }
            });

              _areaModel.GetAreaSaveDataAsReactive().Subscribe(item => 
            {
                if(_areaModel.GetAreaSaveData() != null && item != null)
                {
                    ShowView();
                }
            });
        }

        public void ShowView()
        {
            var areaEssences = _essenceService._registeredEssences.Where(essence => essence as AreaView);

            if(areaEssences != null && areaEssences.Count() != 0)
            {
                foreach(var modelData in _areaModel.GetAreaSaveData().AreaItemDatas)
                {
                    var areaView = areaEssences.FirstOrDefault(item => (item as AreaView).GetObjectName() == modelData.Id) as AreaView;

                     if(areaView == null) return;

                      areaView.UpdateView(new AreaViewArgs
                      {
                            Id = modelData.Id,

                            Name = modelData.Name,

                            Description = modelData.Description,

                            CurLevel  = modelData.MinLevel,

                            CurHealthPoint = modelData.CurHealthPoint,

                            MaxHealthPoint = modelData.MaxHealthPoint,

                            IsInteractive = modelData.IsInteractive,

                            StatusType = modelData.StatusType,

                            StageType  = modelData.StageType,

                             AreaType = modelData.AreaType
            
                      });
                }
                   
            }
        } 
    }
}
