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

namespace Services.Ability
{
    public class PlayerBaseAttackAbility : IAbilityWithOutParam
    {
        private SignalBus _signalBus; 
        
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly ItemService _itemService;
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
             LogService logservice,
             AbilitySettings[] abilitiesSettings) 
        { 
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _itemService = itemService;
            _logService = logservice;

            InitAbility(abilitiesSettings); 
        }
        public void InitAbility(AbilitySettings[] abilitiesSettings) 
        {
           Id = this.GetType().Name;

           _abilitySettings = abilitiesSettings.FirstOrDefault(s => s.Id == Id);

           AbilityType = _abilitySettings.AbilityType;

           WeaponType = _abilitySettings.WeaponType;

           Icon = _abilitySettings.Icon;
        }

        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;

            var presenter =  ownerPresenter;

            if (presenter != null)
            {
                var view = (PlayerView) presenter.GetView();

                // TODO:
                switch(actionModifier)
                {
                    case ActionModifier.SingleFire:
                    {
                        // TODO:
                        break;
                    }

                    case ActionModifier.BurstFire:
                    {
                        // TODO:
                        break;
                    }

                    case ActionModifier.UltaFire:
                    {
                        // TODO:
                        break;
                    }
                }
            }
        }
    }
}