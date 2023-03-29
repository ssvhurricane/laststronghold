using Signals;
using UnityEngine;
using Zenject;

namespace Services.RayCast
{
    public class ReceiverHolder : MonoBehaviour
    {
        [SerializeField] protected GameObject Parent, ReceiverObject;
        [SerializeField] protected ReceiverType ReceiverType;

        [SerializeField] protected int ObjectId;

        [SerializeField] protected string ObjectName;

        private SignalBus _signalBus;
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Fire(new RayCastServiceSignals.AddReceiver(this));
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
        
        public int GetId()
        {
            return ObjectId;
        }

        public string GetObjectName()
        {
            return ObjectName;
        }
    }
}
