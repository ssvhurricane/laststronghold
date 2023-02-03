using System.Collections.Generic;
using Services.Log;
using UnityEngine;
using Zenject;

namespace Services.RayCast
{
    public class RayCastService
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        public RayCastService(SignalBus signalBus, LogService logService)
        {
            _signalBus = signalBus;

            _logService = logService;
        }

        public RaycastHit Emit(Transform transform, LayerMask layerMask)
        {
            RaycastHit hit;
         
            if (Physics.Raycast(transform.position,
                transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 1 << layerMask))
                return hit;
         
            return hit;
        }
    }
}