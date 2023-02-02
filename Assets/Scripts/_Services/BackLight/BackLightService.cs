using UnityEngine;
using Zenject;

namespace Services.BackLight
{
    public class BackLightService
    {
        private readonly SignalBus _signalBus;

        private GameObject _lastObject;
        public BackLightService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Light(GameObject obj, bool enabled)
        {
            if (enabled)
            {
                _lastObject = obj;
                obj.GetComponent<Renderer>().material.color = Color.grey;
            }
            else
            {
                if (_lastObject) _lastObject.GetComponent<Renderer>().material.color = Color.white;
                _lastObject = null;
            }
        }
    }
}
