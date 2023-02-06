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
using Services.Log;

namespace Services.Ability
{
    public class PlayerFocusMoveAbility : IAbilityWithVector3Param, IAbilityWithTransformParam
    {
        private SignalBus _signalBus;

        private readonly MovementService _movementService;
        private readonly AnimationService _animationService;
        private readonly SFXService _sFXService;
        private readonly VFXService _vFXService;
        private readonly LogService _logService;

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
             LogService logService,
             AbilitySettings[] abilitiesSettings)
        {
            _signalBus = signalBus;

            _movementService = movementService;
            _movementServiceSettings = _movementService.InitService(MovementServiceConstants.BasePlayerMovement);

            _animationService = animationService;
            _sFXService = sFXService;
            _vFXService = vFXService;
            _logService = logService;

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
            
                _view.GetHeliCopter().transform.localRotation = Quaternion.Euler(.0f, -90f, _view.transform.rotation.eulerAngles.x);// TODO: ref
                                                                                                    
                switch (actionModifier)
                {
                    case ActionModifier.FocusMove:
                        {
                            // TODO:
                            var innerVector = new Vector3(_movementServiceSettings.OrbitAnchor.Radius,
                                _movementServiceSettings.OrbitAnchor.Anchor.transform.position.y,
                                _movementServiceSettings.OrbitAnchor.Radius);

                            var resultPosition = _view.transform.forward * Vector3.Distance(param, _view.transform.position) + innerVector;

                            resultPosition += new Vector3(.0f, innerVector.y, .0f);

                            _movementService.MoveToWardsWithRadius(_view, resultPosition);

                            if (_view.GetGameObject().transform.position == resultPosition)
                            {
                                // TODO: dont work log
                                _logService.ShowLog(GetType().Name,
                                   Services.Log.LogType.Message, "Stop Moving: " + _view.name,
                                   LogOutputLocationType.Console);
                              
                                ActivateAbility = false;
                            }

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
                            _movementService.RotateToWardsWithDirection(_view, param);

                            break;
                        }
                }
            }
        }
    }
}