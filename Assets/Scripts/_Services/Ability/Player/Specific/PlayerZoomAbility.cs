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
using System.Collections.Generic;
using Services.Pool;
using Services.Resources;
using Services.RayCast;
using Services.Interaction;
using View.Camera;
using Presenters.Window;
using View.Window;

namespace Services.Ability
{
    public class PlayerZoomAbility : IAbilityWithOutParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly ItemService _itemService;
        private readonly PoolService _poolService;
        private readonly ResourcesService _resourcesService;
        private readonly RayCastService _rayCastService;
        private readonly InteractionService _interactionService;
        private AbilitySettings _abilitySettings;

        private readonly MainHUDPresenter _mainHUDPresenter;
       
        private IEnumerable<IItem> _playerWeaponItems;
       
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }
        public bool ActivateAbility { get; set; } = true;
   
        public PlayerZoomAbility(SignalBus signalBus,
           MovementService movementService,
           AnimationService animationService,
           SFXService sFXService,
           VFXService vFXService,
           ItemService itemService,
           PoolService poolService,
           ResourcesService resourcesService,
           RayCastService rayCastService,
           InteractionService interactionService,
            AbilitySettings[] abilitiesSettings,
            MainHUDPresenter mainHUDPresenter)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _itemService = itemService;
            _poolService = poolService;
            _resourcesService = resourcesService;
            _rayCastService = rayCastService;
            _interactionService = interactionService;

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
        }

        public void StartAbility(IPresenter ownerPresenter, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;
            
            var presenter = ownerPresenter;

            if (presenter != null)
            { 
                var view = (FPSCameraView) presenter.GetView();

                if(!_animationService.GetBool(view.GetAnimator(), "Zoom"))
                {
                    _animationService.SetBool(view.GetCameraAnimator(), "Zoom", true);

                    _animationService.SetBool(view.GetAnimator(), "Zoom", true);

                    (_mainHUDPresenter.GetView() as MainHUDView).GetAim().gameObject.SetActive(false);
                }
                else
                {
                    _animationService.SetBool(view.GetCameraAnimator(), "Zoom", false);

                    _animationService.SetBool(view.GetAnimator(), "Zoom", false);

                    (_mainHUDPresenter.GetView() as MainHUDView).GetAim().gameObject.SetActive(true);
                } 
            }
        }
    }
}