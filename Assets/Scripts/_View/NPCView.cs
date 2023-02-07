using Services.Essence;
using Services.NPC;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class NPCView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] protected NPCType NPCType;

        [SerializeField] protected NPCMode NPCMode;
       
        [SerializeField] protected Animator Animator;

        private SignalBus _signalBus;
      
        [Inject]
        public void Constrcut(SignalBus signalBus)
            
        {
            _signalBus = signalBus;
           
             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public Animator GetAnimator()
        {
            return Animator;
        }

        public EssenceType GetEssenceType ()
        {
            return EssenceType;
        }

        public NPCType GetNPCType()
        {
            return NPCType;
        }

        public NPCMode GetNPCMode()
        {
            return NPCMode;
        }
    }
}
