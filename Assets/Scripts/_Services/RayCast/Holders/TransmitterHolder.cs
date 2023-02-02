using UnityEngine;
using Zenject;

namespace Services.RayCast
{
    public class TransmitterHolder : MonoBehaviour
    {
        [SerializeField] protected GameObject EmitObject;
        [SerializeField] protected TransmitterType TransmitterType;

        [SerializeField] protected int ObjectId;

        [SerializeField] protected string ObjectName;

        private SignalBus _signalBus;

        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public GameObject GetEmitObject()
        {
            return EmitObject;
        }

        public TransmitterType GetTransmitterType()
        {
            return TransmitterType;
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