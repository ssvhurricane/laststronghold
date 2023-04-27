using System.Collections.Generic;
using Model;
using Services.Log;
using Signals;
using UnityEngine;
using Zenject;
using UniRx;
using Services.Cheat;
using Constants;
using System.Linq;
using Data.Settings;

namespace Services.RayCast
{
    public class RayCastService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly CheatService _cheatService;
        
        private readonly ReceiverModel _receiverModel;

        private List<ReceiverHolder> _receiverHolders;

        private List<TransmitterHolder> _transmitterHolders;
        private readonly ReceiverHolderSettings[] _receiverHolderSettings;

        public RayCastService(SignalBus signalBus, 
                              LogService logService, 
                              CheatService cheatService,
                              ReceiverModel receiverModel,
                              ReceiverHolderSettings[] receiverHolderSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _cheatService = cheatService;

            _receiverModel = receiverModel;

            _receiverHolderSettings = receiverHolderSettings;

            _receiverHolders = new List<ReceiverHolder>();

            _transmitterHolders = new List<TransmitterHolder>(); 
            
            AddCheats();

            _signalBus.Subscribe<RayCastServiceSignals.AddReceiver>(signal =>
            {
                _receiverHolders.Add(signal.ReceiverHolder); 
               
            });

            _signalBus.Subscribe<RayCastServiceSignals.AddTransmitter>(signal =>
            {
                _transmitterHolders.Add(signal.TransmitterHolder);
            });

             _signalBus.Subscribe<SceneServiceSignals.SceneLoadingCompleted>(data =>
            {
                
                if (data.Data == SceneServiceConstants.OfflineLevel1)
                {
                    InitializeReceivers();
                }
            });

             // ReceiverData.
            _receiverModel.GetReceiverSaveDataAsReactive().Subscribe(item => 
            {
                if(_receiverModel.GetReceiverSaveData() != null && item != null)
                {
                    InitializeReceivers();
                }
            });
        }

        private void InitializeReceivers()
        {
            if(_receiverHolders != null && _receiverHolders.Count != 0)
            {
              foreach(var modelData in _receiverModel.GetReceiverSaveData().ReceiverItemDatas)
                    _receiverHolders.FirstOrDefault(item => item.GetObjectName() == modelData.Id).EnableReceiver(modelData.IsEnabled);
            }
        }

        public List<ReceiverHolder> GetReceiverHolders()
        {
            return _receiverHolders;
        }

        public List<TransmitterHolder> GetTransmitterHolders()
        {
            return _transmitterHolders;
        }

        public RaycastHit Emit(Transform transform, LayerMask layerMask)
        {
            RaycastHit hit;
         
            if (Physics.Raycast(transform.position,
                transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                return hit;
         
            return hit;
        }

         public RaycastHit Emit(Transform transform)
        {
            RaycastHit hit;
         
            if (Physics.Raycast(transform.position,
                transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                return hit;
         
            return hit;
        }

         public RaycastHit Emit(Ray ray)
        {
            RaycastHit hit;
         
            if (Physics.Raycast(ray, out hit))
                return hit;
         
            return hit;
        }

        private void AddCheats()
        {
            var dropDownItems = new List<string>();

            foreach(var receiverItemData in _receiverHolderSettings)
            {
                    dropDownItems.Add(receiverItemData.Receiver.Id);
            }

            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
                        .SetDropdownOptions(dropDownItems)
                        .SetButtonName("Enable Selected Receiver: ")
                        .SetButtonCallback(() =>
                        { 
                            var rModelData = _receiverModel.GetReceiverSaveData();

                            rModelData.ReceiverItemDatas.FirstOrDefault(data =>data.Id == button.CurItemText).IsEnabled = true;

                            _receiverModel.UpdateModelData(rModelData);
                           
                        }), CheatServiceConstants.Receivers);
            
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
                        .SetDropdownOptions(dropDownItems)
                        .SetButtonName("Disable Selected Receiver: ")
                        .SetButtonCallback(() =>
                        {
                            var rModelData = _receiverModel.GetReceiverSaveData();

                            rModelData.ReceiverItemDatas.FirstOrDefault(data => data.Id == button.CurItemText).IsEnabled = false;

                            _receiverModel.UpdateModelData(rModelData);

                        }), CheatServiceConstants.Receivers);   
                        
                        
              _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Enable All Receivers")
            .SetButtonCallback(() =>
            { 
                var rModelData = _receiverModel.GetReceiverSaveData();

                foreach(var data in rModelData.ReceiverItemDatas) data.IsEnabled = true;

                _receiverModel.UpdateModelData(rModelData);
                
            }), CheatServiceConstants.Receivers);

             _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Disable All Receivers")
            .SetButtonCallback(() =>
            {

                  var rModelData = _receiverModel.GetReceiverSaveData();

                   foreach(var data in rModelData.ReceiverItemDatas) data.IsEnabled = false;

                    _receiverModel.UpdateModelData(rModelData);
                
            }), CheatServiceConstants.Receivers);
        
        }
    }
}