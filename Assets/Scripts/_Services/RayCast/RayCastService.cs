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

        public void Emit(Transform transform)
        {
            // TODO:

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                _logService.ShowLog(GetType().Name,
                              Services.Log.LogType.Message,
                              "Did Hit",
                              LogOutputLocationType.Console);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

                _logService.ShowLog(GetType().Name,
                              Services.Log.LogType.Message,
                              "Did not Hit",
                              LogOutputLocationType.Console);
            }
        }
    }
}