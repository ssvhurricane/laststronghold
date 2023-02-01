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
using Constants;
using Services.Anchor;

namespace Services.Ability
{
    public class PlayerLookAtAbility : IAbilityWithOutParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;

        private AbilitySettings _abilitySettings;

        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public ActionModifier ActionModifier { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public Sprite Icon { get; set; }

        private PlayerView _view;
        private MovementServiceSettings _movementServiceSettings;

        public PlayerLookAtAbility(SignalBus signalBus,
             MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
              AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;

            InitAbility(abilitiesSettings);

            _movementServiceSettings = _movementService.InitService(MovementServiceConstants.BasePlayerMovement);
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
            if (ownerPresenter != null)
            {
                if (_view == null) _view = (PlayerView)ownerPresenter.GetView();

               _movementService.LookAt(_view, Vector3.zero, Vector3.up);
            }
        }
    }
}