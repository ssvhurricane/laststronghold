using System.Collections.Generic;
using Constants;
using Model;
using Services.Cheat;
using Zenject;
using UniRx;
using Data.Settings;
using System.Linq;
using Signals;
using Services.Essence;
using View;
using System;

namespace Services.Area
{
    public class AreaService
    {
        private readonly SignalBus _signalBus;

        private readonly CheatService _cheatService;

        private readonly EssenceService _essenceService;
        
        private readonly AreaModel _areaModel;
        private readonly ReceiverModel _receiverModel;
        private readonly AreaServiceSettings[] _areaServiceSettings;

        public AreaService(SignalBus signalBus,
                         CheatService cheatService,
                         EssenceService essenceService,
                         AreaModel areaModel,
                         ReceiverModel receiverModel,
                         AreaServiceSettings[] areaServiceSettings)
        {
            _signalBus = signalBus;

            _cheatService = cheatService;
            _essenceService = essenceService;

            _areaModel = areaModel;

            _receiverModel = receiverModel;

            _areaServiceSettings = areaServiceSettings;

            AddCheats();

            // ReceiverData.
            _receiverModel.GetReceiverSaveDataAsReactive().Subscribe(item => 
            {
                if(_receiverModel.GetReceiverSaveData() != null && item != null)
                {
                    // TODO:
                }
            });

             // AreaData.
            _areaModel.GetAreaSaveDataAsReactive().Subscribe(item => 
            {
                if(_areaModel.GetAreaSaveData() != null && item != null)
                {
                    InitializeAreas();
                }
            });

            _signalBus.Subscribe<SceneServiceSignals.SceneLoadingCompleted>(data =>
            {
                
                if (data.Data == SceneServiceConstants.OfflineLevel1)
                {
                    InitializeAreas();
                }
            });
        }

        private void InitializeAreas()
        {
            var areaEssences = _essenceService._registeredEssences.Where(essence => essence as AreaView);

            if(areaEssences != null && areaEssences.Count() != 0)
            {
                foreach(var modelData in _areaModel.GetAreaSaveData().AreaItemDatas)
                    AreaProcessing(areaEssences.FirstOrDefault(item => (item as AreaView).GetObjectName() == modelData.Id), modelData.IsInteractive);
            }
        }

        public void AreaProcessing(IEssence areaView, bool isInteractive)
        {
            if(areaView != null)
            {
                var area = areaView as AreaView;

                if(area != null)
                {
                     area.InteractiveArea(isInteractive);

                      if(isInteractive) area.StartSimulate();
                     
                      else area.StopSimulate();
                }
            }
        }

        private void AddCheats()
        {
            var dropDownItems = new List<string>();

            foreach(var areaItemData in _areaServiceSettings) dropDownItems.Add(areaItemData.Area.Id);
         
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(dropDownItems)
               .SetButtonName("Interactive Selected Area:")
               .SetButtonCallback(() =>
               {
                    var aModelData = _areaModel.GetAreaSaveData();

                    aModelData.AreaItemDatas.FirstOrDefault(data =>data.Id == button.CurItemText).IsInteractive = true;

                    _areaModel.UpdateModelData(aModelData);

               }), CheatServiceConstants.Areas);

         
           _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(dropDownItems)
               .SetButtonName("None Interactive Selected Area:")
               .SetButtonCallback(() =>
               {
                    var aModelData = _areaModel.GetAreaSaveData();

                    aModelData.AreaItemDatas.FirstOrDefault(data =>data.Id == button.CurItemText).IsInteractive = false;

                    _areaModel.UpdateModelData(aModelData);

               }), CheatServiceConstants.Areas);
  
            _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Interactive All Areas")
            .SetButtonCallback(() =>
            {
                 var aModelData = _areaModel.GetAreaSaveData();

                 foreach(var data in aModelData.AreaItemDatas) data.IsInteractive = true;

                _areaModel.UpdateModelData(aModelData);

            }), CheatServiceConstants.Areas);
          
            _cheatService.AddCheatItemControl<CheatButtonDoubleDropdownControl>(button => button  
             .SetFirstDropdownOptions(dropDownItems)
             .SetSecondDropdownOptions(Enum.GetNames(typeof(StatusType)).ToList())
            .SetButtonName("Change status Area:")
            .SetButtonCallback(() =>
               {
                   var aModelData = _areaModel.GetAreaSaveData();

                   aModelData.AreaItemDatas
                   .FirstOrDefault(data => data.Id == button.CurFirstItemText).StatusType
                    = (StatusType)Enum.Parse(typeof(StatusType), button.CurSecondItemText, true);

                    _areaModel.UpdateModelData(aModelData);

               }), CheatServiceConstants.Areas);
        }
    }
}
