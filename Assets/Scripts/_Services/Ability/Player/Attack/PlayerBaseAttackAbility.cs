using Data.Settings;
using Presenters;
using Services.Animation;
using Services.Input;
using Services.Movement;
using Services.SFX;
using Services.VFX;
using Services.Item.Weapon;
using System.Linq;
using UnityEngine;
using View;
using Zenject;
using Services.Item;
using Services.Log;
using Services.RayCast;
using Services.Shooting;
using Presenters.Window;
using Constants;

namespace Services.Ability
{
    public class PlayerBaseAttackAbility : IAbilityWithOutParam, IAbilityWithBoolParam
    {
        private SignalBus _signalBus; 
        
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly ItemService _itemService;
        private readonly ShootingService _shootingService;

        private readonly MainHUDPresenter _mainHUDPresenter;
         
        private LogService _logService;

        private AbilitySettings _abilitySettings;
     
        public string Id { get ; set; }
        public AbilityType AbilityType { get ; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }
        
        public PlayerBaseAttackAbility(SignalBus signalBus,
             MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
             ItemService itemService,
             RayCastService rayCastService,
             ShootingService shootingService,
             LogService logservice,
             MainHUDPresenter mainHUDPresenter,
             AbilitySettings[] abilitiesSettings) 
        { 
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _itemService = itemService;
      
            _shootingService = shootingService;
            _logService = logservice;

            _mainHUDPresenter = mainHUDPresenter;

            InitAbility(abilitiesSettings); 
        }
        public void InitAbility(AbilitySettings[] abilitiesSettings) 
        {
           Id = this.GetType().Name;

           _abilitySettings = abilitiesSettings.FirstOrDefault(s => s.Id == Id);

           AbilityType = _abilitySettings.AbilityType;

           WeaponType = _abilitySettings.WeaponType;

           Icon = _abilitySettings.Icon;

           _shootingService.Initialize(PoolServiceConstants.RPGBulletItemViewPool);

           _shootingService.Initialize(PoolServiceConstants.MDBulletItemViewPool);

           _shootingService.Initialize(PoolServiceConstants.RPGBulletItemViewPool);
        }

        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;

            var presenter =  ownerPresenter;

            if (presenter != null)
            {
                var view = (PlayerView) presenter.GetView();

                if(_mainHUDPresenter._sniperRifleItemView.IsActive)
                    _shootingService.Shoot(ActionModifier.SingleFire, ItemServiceConstants.SniperRifleItemView, PoolServiceConstants.RPGBulletItemViewPool);

                if(_mainHUDPresenter._rPGItemView.IsActive)
                    _shootingService.Shoot(ActionModifier.UltaFire,  ItemServiceConstants.RPGItemView, PoolServiceConstants.RPGBulletItemViewPool);
            }
        }

        public void StartAbility(IPresenter ownerPresenter, bool param, ActionModifier actionModifier)
        {  
            if (!ActivateAbility) return;

            if(_mainHUDPresenter._mDItemView.IsActive)
                    _shootingService.Shoot(ActionModifier.BurstFire, ItemServiceConstants.MDItemView, PoolServiceConstants.MDBulletItemViewPool);
        }
    }
}