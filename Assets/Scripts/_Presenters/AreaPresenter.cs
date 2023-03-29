using Model;
using Zenject;
using Services.Log;
using Services.Essence;
using Services.Anchor;
using Services.Pool;
using Services.Factory;
using System.Collections.Generic;
using View;

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

        private List<AreaView> _areaViews;

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
        }

        public void ShowView()
        {
            // TODO:
        } 
    }
}
