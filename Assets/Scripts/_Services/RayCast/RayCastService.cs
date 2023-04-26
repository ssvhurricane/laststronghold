using System.Collections.Generic;
using Model;
using Services.Log;
using Signals;
using UnityEngine;
using Zenject;
using UniRx;

namespace Services.RayCast
{
    public class RayCastService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        
        private readonly ReceiverModel _receiverModel;

        private List<ReceiverHolder> _receiverHolders;

        private List<TransmitterHolder> _transmitterHolders;

        public RayCastService(SignalBus signalBus, LogService logService, ReceiverModel receiverModel)
        {
            _signalBus = signalBus;

            _logService = logService;

            _receiverModel = receiverModel;

            _receiverHolders = new List<ReceiverHolder>();

            _transmitterHolders = new List<TransmitterHolder>();

            _signalBus.Subscribe<RayCastServiceSignals.AddReceiver>(signal =>
            {
                _receiverHolders.Add(signal.ReceiverHolder);
            });

            _signalBus.Subscribe<RayCastServiceSignals.AddTransmitter>(signal =>
            {
                _transmitterHolders.Add(signal.TransmitterHolder);
            });

             // ReceiverData.
            _receiverModel.GetReceiverSaveDataAsReactive().Subscribe(item => 
            {
                if(_receiverModel.GetReceiverSaveData() != null && item != null)
                {
                    // TODO:
                }
            });
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
    }
}