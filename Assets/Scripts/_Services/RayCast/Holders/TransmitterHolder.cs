using UnityEngine;

namespace Services.RayCast
{
    public class TransmitterHolder : MonoBehaviour
    {
        [SerializeField] protected GameObject EmitObject;
        [SerializeField] protected TransmitterType TransmitterType;

        private int _objectId;

        private string _objectName;

        public void Construct()
        {
            // TODO:need reg holder

        }
    }
}