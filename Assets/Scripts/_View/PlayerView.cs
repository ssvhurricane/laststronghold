using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class PlayerView : NetworkEssence, ITakingDamage
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] protected GameObject Helicopter;
        [SerializeField] protected GameObject PlayerModel;
        [SerializeField] protected GameObject CameraRoot;

        [SerializeField] protected Animator Animator;

        private SignalBus _signalBus;
      
        [Inject]
        public void Constrcut(SignalBus signalBus)
            
        {
            _signalBus = signalBus;
           
             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public GameObject GetHeliCopter()
        {
            return Helicopter;
        }

        public GameObject GetPlayerModel()
        {
            return PlayerModel;
        }

        public GameObject GetCameraRoot() 
        {
            return CameraRoot;
        }

        public Animator GetAnimator()
        {
            return Animator;
        }

        public void Damage(float damageCount)
        {
            throw new System.NotImplementedException();
        }

        public bool Kill()
        {
            throw new System.NotImplementedException();
        }
    }
}
