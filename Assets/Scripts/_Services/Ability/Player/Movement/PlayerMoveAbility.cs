using Constants;
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

namespace Services.Ability
{
    public class PlayerMoveAbility : IAbilityWithBoolParam
    {
        private SignalBus _signalBus;
       
        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService; 
        
        private AbilitySettings _abilitySettings;

        private MovementServiceSettings _movementServiceSettings;
        public string Id { get; set; }
        public AbilityType AbilityType { get; set; }
        public WeaponType WeaponType { get; set; }
        public bool ActivateAbility { get; set; } = true;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        private PlayerView _view;

        public PlayerMoveAbility(SignalBus signalBus,
            MovementService movementService,
             AnimationService animationService,
             SFXService sFXService,
             VFXService vFXService,
             AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _movementServiceSettings = _movementService.InitService(MovementServiceConstants.BasePlayerMovement);

            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;

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

        public void StartAbility(IPresenter ownerPresenter, bool param, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;

            if (ownerPresenter != null)
            {
                _view = (PlayerView)ownerPresenter.GetView();

                if (param)
                {
                    switch (actionModifier)
                    {
                        case ActionModifier.Left:
                            {
                                _movementService.OrbitalMove(_view, Vector3.zero, Quaternion.Euler(.0f, _movementServiceSettings.Rotate.Speed, .0f));
                                break;
                            }
                        case ActionModifier.Right:
                            {
                                _movementService.OrbitalMove(_view, Vector3.zero, Quaternion.Euler(.0f, -_movementServiceSettings.Rotate.Speed, .0f));
                                break;
                            }
                    }
                }
            }
        }
    }
}