using Services.Essence;
using Services.Item.Weapon;
using Signals;
using UnityEngine;
using Zenject;

namespace Services.Item
{
    public class SniperRifleItemView : NetworkEssence, IWeapon  
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] protected Transform TrnasformFirePivot; 

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
        public GameObject Owner { get; set; }
        public WeaponType WeaponType { get; set; }
        public GameObject Prefab { get; set; }
        public Transform FirePivot 
        { 
            get { return TrnasformFirePivot; } 
            set { TrnasformFirePivot = value; }
        }

        private SignalBus _signalBus;
      
        [Inject]
        public void Constrcut(SignalBus signalBus)
            
        {
            _signalBus = signalBus;
           
             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public void UpdateView(){}
    }
}