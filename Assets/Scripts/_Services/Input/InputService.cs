using Constants;
using Data.Settings;
using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Anchor;
using Services.Animation;
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
                                              _cameraRotateAbility;
        
        private IEnumerable<IAbility> _playerAttackAbilities;
    
        private MainHUDView _mainHudView;
        private PlayerView _playerView;
    
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
            AnchorService anchorService
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

            _settings = _inputServiceSettings?.FirstOrDefault(s => s.Id == InputServiceConstants.TopDownGameId);

            _topDownGameInput = new TopDownGameInput();
          
           // Bind Player Base Attack Ability.
           _topDownGameInput.Player.Attack1.performed += value =>
            {
                if (_projectService.GetProjectState() == ProjectState.Start)
                {
                    
                    _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "Press LeftMouseButton(LT).",
                                LogOutputLocationType.Console);
                    /*
                    if (_mainHudView.GetVerticalAbilityPanel().gameObject.activeSelf)
                        _mainHudView.GetVerticalAbilityPanel().gameObject.SetActive(false);

                    if (((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility().Id != AbilityServiceConstants.PlayerNoneAbility)
                        _abilityService.UseAbility((IAbilityWithOutParam)((ILiveModel)_playerPresenter.GetModel()).GetCurrentAbility(), _playerPresenter, ActionModifier.None);
                    */
                }
            };


            // Back Menu.
            _topDownGameInput.Player.Pause.performed += value =>
            {
                if (_windowService.IsWindowShowing<PauseMenuView>()) return;

                if (_windowService.IsWindowShowing<GameSettingsView>()) return;

                _pauseMenuPresenter.ShowView();
            };

            _topDownGameInput.Player.Focus.performed += value =>
            {
                // TODO:
                _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "Press Focus Button(F).",
                                LogOutputLocationType.Console);

                _rayCastService.Emit(_cameraPresenter.GetView().GetGameObject().transform);
                //1.
                //_anchorService.GetActorByName();
                //2.
               // _rayCastService.Emit();
            };

            _topDownGameInput.Player.Reset.performed += value =>
            {
                // TODO:
                //_anchorService.GetActorByName();
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
                if (!_topDownGameInput.Player.Move.IsPressed())
                    _abilityService.UseAbility((IAbilityWithOutParam)_playerIdleAbility, _playerPresenter, ActionModifier.None);
                else
                {
                    if (_topDownGameInput.Player.Move.activeControl.name == "a")
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message,
                                    "Press A.",
                                    LogOutputLocationType.Console);

                        _abilityService.UseAbility((IAbilityWithBoolParam)_playerMoveAbility,
                            _playerPresenter,
                            _topDownGameInput.Player.Move.IsPressed(),
                            ActionModifier.Left);
                    }


                    if (_topDownGameInput.Player.Move.activeControl.name == "d")
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message,
                                    "Press D.",
                                    LogOutputLocationType.Console);

                        _abilityService.UseAbility((IAbilityWithBoolParam)_playerMoveAbility,
                            _playerPresenter,
                            _topDownGameInput.Player.Move.IsPressed(),
                            ActionModifier.Right);
                    }
                }
            }
        }

        public void LateTick()
        {
            if (_projectService.GetProjectState() == ProjectState.Start)
            {
                _abilityService.UseAbility((IAbilityWithOutParam)_playerLookAtAbility, _playerPresenter
                      , ActionModifier.None);

                if (_topDownGameInput.Player.Look.IsPressed())
                {
                    _abilityService.UseAbility((IAbilityWithVector2Param)_cameraRotateAbility
                         , _cameraPresenter,
                    _topDownGameInput.Player.Look.ReadValue<Vector2>(), ActionModifier.None);
                } 
                
                // Set CameraRoot position.
                _abilityService.UseAbility((IAbilityWithAffectedPresenterParam)_cameraRotateAbility
                    , _cameraPresenter, _playerPresenter, ActionModifier.None);

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

            // Caching Player Move Ability.
            _playerMoveAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerMoveAbility);

            _playerLookAtAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerLookAtAbility);

            // Caching Player None Ability.
            _playerNoneAbility = _abilityService.GetAbilityById(_playerPresenter,
                AbilityServiceConstants.PlayerNoneAbility);

            _playerAttackAbilities = _abilityService.GetAbilitiesyByAbilityType(_playerPresenter,
                AbilityType.AttackAbility);

         
            // Caching Camera Rotate Ability.
            _cameraRotateAbility = _abilityService.GetAbilityById(_cameraPresenter,
                AbilityServiceConstants.CameraRotateAbility);
        }
    }
}