using UnityEngine;

namespace Services.RayCast
{
    public class ReceiverHolder : MonoBehaviour
    {
        [SerializeField] protected GameObject Parent, ReceiverObject;
        [SerializeField] protected ReceiverType ReceiverType;

        private int _objectId;

        private string _objectName;

        public void Construct()
        {
            // TODO:need reg holder

        }
    }
}
