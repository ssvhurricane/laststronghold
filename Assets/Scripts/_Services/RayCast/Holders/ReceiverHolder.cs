using Services.Building;
using Signals;
using UnityEngine;
using View;
using Zenject;

namespace Services.RayCast
{
    public class ReceiverHolder : MonoBehaviour, IBuildingHolder
    {
        [SerializeField] protected GameObject Parent, ReceiverObject;
        [SerializeField] protected ReceiverType ReceiverType;

        [SerializeField] protected int ObjectId;

        [SerializeField] protected string ObjectName;

        private SignalBus _signalBus;

        private IView _placedObject;

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

        public int GetId()
        {
            return ObjectId;
        }

        public string GetObjectName()
        {
            return ObjectName;
        }

        public void SetPlacedObject(IView viewObject)
        {
            _placedObject = viewObject;
        }

        public IView GetPlacedObject()
        {
           return _placedObject;
        }

        public bool IsPlacedObjectEmpty()
        {
            return _placedObject is IView;
        }
    }
}
