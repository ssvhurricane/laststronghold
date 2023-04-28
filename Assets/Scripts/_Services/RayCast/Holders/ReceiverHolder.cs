using Signals;
using UnityEngine;
using Zenject;

namespace Services.RayCast
{
    public class ReceiverHolder : MonoBehaviour
    {
        [SerializeField] protected GameObject Parent, ReceiverObject;
        [SerializeField] protected ReceiverType ReceiverType;

        [SerializeField] protected string ObjectId;

        private SignalBus _signalBus;

        public bool IsEnabled { get; private set; } = false;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Fire(new RayCastServiceSignals.AddReceiver(this));
        }
        public void EnableReceiver(bool enabled)
        {
            IsEnabled = enabled;
            
            gameObject.SetActive(enabled);
        }

        public GameObject GetReceiveObject()
        {
            return ReceiverObject;
        }

        public GameObject GetParentObject()
        {
            return Parent;
        }
        public ReceiverType GetReceiverType()
        {
            return ReceiverType;
        }
        
        public string GetId()
        {
            return ObjectId;
        }
    }
}
