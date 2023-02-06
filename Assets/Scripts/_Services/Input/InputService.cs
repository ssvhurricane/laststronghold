using Constants;
using Data.Settings;
using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Anchor;
using Services.Animation;
using Services.BackLight;
using Services.Log;
using Services.Pool;
using Services.Project;
using Services.RayCast;
using Services.Resources;
using Services.Window;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View;
using View.Camera;
using View.Window;
using Zenject;

namespace Services.Input
{
    public class InputService : IFixedTickable, ILateTickable
    {
        private readonly SignalBus _signalBus;

        private readonly InputServiceSettings[] _inputServiceSettings;
        private InputServiceSettings _settings;
       
        private readonly AbilityService _abilityService;
        private readonly PoolService _poolService;
        private readonly LogService _logService;
        private readonly ProjectService _projectService;
        private readonly RayCastService _rayCastService;
        private readonly AnchorService _anchorService;
        private readonly BackLightService _backLightService;
        private readonly IWindowService _windowService;

        private readonly PauseMenuPresenter _pauseMenuPresenter;
        private readonly MainHUDPresenter _mainHUDPresenter;
        private readonly CameraPresenter _cameraPresenter;

        private IPresenter _playerPresenter;
       
        private TopDownGameInput _topDownGameInput;

        private IAbility _playerNoneAbility,
                                _playerIdleAbility,
                                    _playerLookAtAbility,
                                         _playerMoveAbility,
                                            _playerFocusMoveAbility,
                                                  _cameraRotateAbility;
       
        private IEnumerable<IAbility> _playerAttackAbilities;
    
        private MainHUDView _mainHudView;
        private PlayerView _playerView;

        private TransmitterHolder _transmitterCamera;
        private ReceiverHolder _receiverAnchorArea;
        private Services.Anchor.Anchor _anchorCenter;

        private Dictionary<int, PlayerAbilityItemView> _playerAbilityItems = new Dictionary<int, PlayerAbilityItemView>();
        KeyValuePair<int, PlayerAbilityItemView> _playerAbility;

        public InputService(SignalBus signalBus,
            InputServiceSettings[] inputServiceSettings,
            AbilityService abilityService,
            AnimationService animationService,
            IWindowService windowService,
            PauseMenuPresenter pauseMenuPresenter,
            MainHUDPresenter mainHUDPresenter,
            CameraPresenter cameraPresenter,
            PoolService poolService,
            ResourcesService resourcesService,
            LogService logService,
            ProjectService projectService,
            RayCastService rayCastService,
            AnchorService anchorService,
            BackLightService backLightService
            )
        {
            _signalBus = signalBus;
            _inputServiceSettings = inputServiceSettings;

            _abilityService = abilityService;
            _windowService = windowService;

            _pauseMenuPresenter = pauseMenuPresenter;
            _mainHUDPresenter = mainHUDPresenter;
            _cameraPresenter = cameraPresenter;

            _poolService = poolService;
            _logService = logService;
            _projectService = projectService;
            _rayCastService = rayCastService;
            _anchorService = anchorService;
            _backLightService = backLightService;

            _settings = _inputServiceSettings?.FirstOrDefault(s => s.Id == InputServiceConstants.TopDownGameId);

            _anchorCenter = new Services.Anchor.Anchor();

            _topDownGameInput = new TopDownGameInput();
           
            _topDownGameInput.Player.Attack1.performed += value =>
            {
                if (_projectService.GetProjectState() == ProjectState.Start)
                {
                    
                    _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "Press LeftMouseButton(LT).",
                                LogOutputLocationType.Console);
                }
            };

            _topDownGameInput.Player.Pause.performed += value =>
            {
                if (_windowService.IsWindowShowing<PauseMenuView>()) return;

                if (_windowService.IsWindowShowing<GameSettingsView>()) return;

                _pauseMenuPresenter.ShowView();
            };

            _topDownGameInput.Player.Focus.performed += value =>
            {
                _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "Press Focus Button(F).",
                                LogOutputLocationType.Console);

                _transmitterCamera = _cameraPresenter.GetView().GetGameObject().GetComponent<TransmitterHolder>();

                if (_transmitterCamera)
                {
                    var hitReceiver = _rayCastService.Emit(_transmitterCamera.transform, LayerMask.NameToLayer("AnchorArea"));

                    _receiverAnchorArea = hitReceiver.collider?.gameObject.GetComponent<ReceiverHolder>();

                    if (_receiverAnchorArea)
                    {
                        _anchorCenter = _anchorService.GetActorByName(_receiverAnchorArea.GetParentObject().name).FirstOrDefault();

                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Receiver Id: " + _receiverAnchorArea.GetId() + " | " +
                                    "Receiver Name: " + _receiverAnchorArea.GetObjectName(),
                                    LogOutputLocationType.Console);
                    }

                    _playerFocusMoveAbility.ActivateAbility = true;

                    _playerLookAtAbility.ActivateAbility = false;

                    _playerMoveAbility.ActivateAbility = false;
                }
            };

            _topDownGameInput.Player.Reset.performed += value =>
            {
                 _receiverAnchorArea = _rayCastService._receiverHolders.FirstOrDefault(item => item.GetId() == 1);

                if (_receiverAnchorArea)
                {
                     _anchorCenter = _anchorService.GetActorByName(_receiverAnchorArea.GetParentObject().name).FirstOrDefault();

                    _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message, "Receiver(reset) Id: " + _receiverAnchorArea.GetId() + " | " +
                                "Receiver Name: " + _receiverAnchorArea.GetObjectName(),
                                LogOutputLocationType.Console);
                }

                _playerFocusMoveAbility.ActivateAbility = true;

                _playerLookAtAbility.ActivateAbility = false;

                _playerMoveAbility.ActivateAbility = false;
            };
        }

        public void ClearServiceValues()
        {
            _playerAbilityItems.Clear();

            _topDownGameInput.Disable();
        }

        public void FixedTick()
        {
            if (_projectService.GetProjectState() == ProjectState.Start)
            {
                // Move helicopter.
                if (!_topDownGameInput.Player.Move.IsPressed())
                    _abilityService.UseAbility((IAbilityWithOutParam)_playerIdleAbility, _playerPresenter, ActionModifier.None);
                else
                {
                    if (_topDownGameInput.Player.Move.activeControl.name == "a")
                    {
                        _abilityService.UseAbility((IAbilityWithVector3Param)_playerMoveAbility,
                            _playerPresenter,
                            _anchorCenter.Transform?.position ?? Vector3.zero,
                            ActionModifier.Left);
                    }

                    if (_topDownGameInput.Player.Move.activeControl.name == "d")
                    {
                        _abilityService.UseAbility((IAbilityWithVector3Param)_playerMoveAbility,
                            _playerPresenter,
                            _anchorCenter.Transform?.position ?? Vector3.zero,
                            ActionModifier.Right);
                    }
                }

                // Select anchor Area.
                var anchorArea = _rayCastService.Emit(_cameraPresenter.GetView().GetGameObject().transform, LayerMask.NameToLayer("AnchorArea")).collider?.gameObject;

                if (anchorArea) _backLightService.Light(anchorArea, true);
                    
                else  _backLightService.Light(anchorArea, false);

                // Move object.
                if (_receiverAnchorArea != null)
                {

                    _abilityService.UseAbility((IAbilityWithVector3Param)_playerFocusMoveAbility,
                            _playerPresenter,
                            _receiverAnchorArea.gameObject.transform.position,
                         ActionModifier.FocusMove);
                }
              
                if (!_playerFocusMoveAbility.ActivateAbility) _playerMoveAbility.ActivateAbility = true;
            }
        }

        public void LateTick()
        {
            if (_projectService.GetProjectState() == ProjectState.Start)
            {
                // Look At.
                 _abilityService.UseAbility((IAbilityWithVector3Param)_playerLookAtAbility,
                    _playerPresenter,
                    _anchorCenter.Transform?.position ?? Vector3.zero
                    , ActionModifier.None);

                // Camera Rotate.
                if (_topDownGameInput.Player.Look.IsPressed())
                {
                    _abilityService.UseAbility((IAbilityWithVector2Param)_cameraRotateAbility
                         , _cameraPresenter,
                    _topDownGameInput.Player.Look.ReadValue<Vector2>(), ActionModifier.None);
                } 
                
                // Set CameraRoot position.
                _abilityService.UseAbility((IAbilityWithAffectedPresenterParam)_cameraRotateAbility
                    , _cameraPresenter, _playerPresenter, ActionModifier.None);

                
                // Rotate direct.
                if(_receiverAnchorArea != null)
                    _abilityService.UseAbility((IAbilityWithTransformParam)_playerFocusMoveAbility,
                          _playerPresenter,
                          _receiverAnchorArea.gameObject.transform,
                          ActionModifier.FocusRotate);
                
                if (!_playerFocusMoveAbility.ActivateAbility)
                    _playerLookAtAbility.ActivateAbility = true;
            }  
        }

        public void TakePossessionOfObject(IPresenter presenter)
        {
            _playerPresenter = (PlayerPresenter) presenter; 
            
            _playerView = (PlayerView) _playerPresenter.GetView();

            _cameraPresenter.ShowView<FPSCameraView>(CameraServiceConstants.FPSCamera, _playerView);

            CachingAbilities();

          //  InitializePlayerAbilityViews();

            _topDownGameInput.Enable();
        }

        //TODO:del,ref.
        private void InitializePlayerAbilityViews() 
        {
            PlayerAbilityItemView playerAbilityItemView;

            _mainHudView = (MainHUDView)_mainHUDPresenter.GetView();
            _mainHudView.GetVerticalAbilityPanel().gameObject.SetActive(false);

            // Init Player Ability.
            ((ILiveModel)_playerPresenter.GetModel())
                .SetCurrentAbility(_playerNoneAbility);

            _mainHudView.GetPlayerAbilityContainer().GetComponent<Image>().sprite = ((ILiveModel) _playerPresenter.GetModel()).GetCurrentAbility().Icon;

            _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.None);

            _poolService.InitPool(PoolServiceConstants.PlayerAbilityItemViewPool);

            for (var item = 0; item < _playerAttackAbilities.Count(); item++) 
            {
                 playerAbilityItemView
                    = (PlayerAbilityItemView)_poolService.Spawn<PlayerAbilityItemView>(_mainHudView.GetVerticalAbilityPanel().transform);

                 playerAbilityItemView._image.sprite = _playerAttackAbilities.ToList()[item].Icon;

                 playerAbilityItemView.Id =_playerAttackAbilities.ToList()[item].Id;

                 _playerAbilityItems.Add(item, playerAbilityItemView);
            }
        }

        private void CachingAbilities()
        {
            // Caching Player Idle Ability.
            _playerIdleAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerIdleAbility);
            _playerIdleAbility.ActivateAbility = true;

            // Caching Player Move Ability.
            _playerMoveAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerMoveAbility);
            _playerMoveAbility.ActivateAbility = true;

           _playerFocusMoveAbility = _abilityService.GetAbilityById(_playerPresenter,
               AbilityServiceConstants.PlayerFocusMoveAbility);
            _playerFocusMoveAbility.ActivateAbility = true;

            _playerLookAtAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerLookAtAbility);
            _playerLookAtAbility.ActivateAbility = true;

            // Caching Player None Ability.
            _playerNoneAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerNoneAbility);
            _playerNoneAbility.ActivateAbility = true;

            _playerAttackAbilities = _abilityService.GetAbilitiesyByAbilityType(_playerPresenter,
                AbilityType.AttackAbility);
         
            // Caching Camera Rotate Ability.
            _cameraRotateAbility = _abilityService.GetAbilityById(_cameraPresenter,
                AbilityServiceConstants.CameraRotateAbility);
            _cameraRotateAbility.ActivateAbility = true;
        }
    }
}