using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class RifleBulletItemView : BaseEssence
    {
        [SerializeField] protected EssenceType Layer;
       
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        void OnCollisionEnter(Collision collision)
        {
             _signalBus.Fire(new ShootingServiceSignals.Hit(this, collision));
        }
    }
}
