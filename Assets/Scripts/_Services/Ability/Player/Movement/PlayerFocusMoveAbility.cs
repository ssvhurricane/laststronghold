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
    public class PlayerFocusMoveAbility : IAbilityWithVector3Param, IAbilityWithTransformParam
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
        public bool ActivateAbility { get; set; } = false;
        public ActionModifier ActionModifier { get; set; }
        public Sprite Icon { get; set; }

        private PlayerView _view;

        public PlayerFocusMoveAbility(SignalBus signalBus,
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

        public void StartAbility(IPresenter ownerPresenter, Vector3 param, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;

            if (ownerPresenter != null)
            {
                _view = (PlayerView)ownerPresenter.GetView();

                switch (actionModifier)
                {
                    case ActionModifier.FocusMove:
                        {
                            // TODO:
                            var result = -_view.transform.forward *
                                Vector3.Distance(param,_view.transform.position)
                                + new Vector3(_movementServiceSettings.OrbitAnchor.Radius,
                                _movementServiceSettings.OrbitAnchor.Anchor.transform.position.y,
                                _movementServiceSettings.OrbitAnchor.Radius);

                            _movementService.MoveToWardsWithRadius(_view, result);

                            break;
                        }
                }
            }
        }

        public void StartAbility(IPresenter ownerPresenter, Transform param, ActionModifier actionModifier)
        {
            if (!ActivateAbility) return;

            if (ownerPresenter != null)
            {
                _view = (PlayerView)ownerPresenter.GetView();

                switch (actionModifier)
                {
                    
                    case ActionModifier.FocusRotate:
                        {
                            // TODO: stop ability then..

                           // if (_view.GetGameObject().transform.forward != (_view.GetGameObject().transform.position - param.position))
                                _movementService.RotateToWardsWithDirection(_view, param);
                          //  else
                         //       ActivateAbility = false;

                            break;
                        }
                }
            }
        }
    }
}