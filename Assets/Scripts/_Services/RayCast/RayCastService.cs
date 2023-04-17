using System.Collections.Generic;
using Services.Log;
using Signals;
using UnityEngine;
using Zenject;

namespace Services.RayCast
{
    public class RayCastService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        public List<ReceiverHolder> _receiverHolders { get; private set; }

        public List<TransmitterHolder> _transmitterHolders { get; private set; }

        public RayCastService(SignalBus signalBus, LogService logService)
        {
            _signalBus = signalBus;

            _logService = logService;

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