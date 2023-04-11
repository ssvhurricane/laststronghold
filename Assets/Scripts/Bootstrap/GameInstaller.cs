using Model;
using Presenters;
using Presenters.Window;
using Services.Ability;
using Services.Movement;
using Utils.Helpers;
using Zenject;
using Services.Animation;
using Services.SFX;
using Services.VFX;
using Services.Detection;
using Services.Item;
using Services.Input;
using Services.Camera;
using Services.Tutorial;
using Services.RayCast;
using Services.Area;
using Services.BackLight;
using Services.NPC;
using Services.Quest;
using Services.Message;
using Services.Shooting;
using Services.Interaction;

namespace Bootstrap
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            InitExecutionOrder();

            InstallPresenters();
            InstallServices();
            InstallModels();
            InstallAbilities();
            InstallSignals();
        }
        void InitExecutionOrder() { }
       
        private void InstallPresenters()
        {
            // Window Presenters.
            Container.InstallElementAsSingle<MainHUDPresenter>();
            Container.InstallElementAsSingle<PauseMenuPresenter>();
            Container.InstallElementAsSingle<CheatMenuPresenter>();

            Container.InstallElementAsSingle<QuestsPresenter>();
            Container.InstallElementAsSingle<QuestsMenuPresenter>();

            Container.InstallElementAsSingle<TutorialPresenter>();
            Container.InstallElementAsSingle<MessagePresenter>();

            // View Presenters.
            Container.InstallElementAsSingle<PlayerPresenter>();
           
            Container.InstallElementAsSingle<CameraPresenter>();

            Container.InstallElementAsSingle<NPCPresenter>();

            Container.InstallElementAsSingle<AreaPresenter>();
        }

        private void InstallServices() 
        {
            Container.InstallElementAsTransient<MovementService>(); 
            Container.InstallElementAsTransient<ShootingService>(); 
            Container.InstallElementAsSingle<AbilityService>();
            Container.InstallElementAsSingle<ItemService>();
            Container.InstallElementAsSingle<InteractionService>();
           
            Container.InstallElementAsSingle<AnimationService>();
            Container.InstallElementAsSingle<SFXService>();
            Container.InstallElementAsSingle<VFXService>();
            Container.InstallElementAsSingle<DetectionService>();

            Container.InstallElementAsSingle<CameraService>();

            Container.InstallElementAsSingle<InputService>(); 

            Container.InstallElementAsSingle<TutorialService>();
            
            Container.InstallElementAsSingle<MessageService>();

            Container.InstallElementAsSingle<RayCastService>();

            Container.InstallElementAsSingle<AreaService>();

            Container.InstallElementAsSingle<BackLightService>();

            Container.InstallElementAsSingle<NPCService>();

            Container.InstallElementAsSingle<QuestService>();
        }

        private void InstallAbilities() 
        {
            // Player Abilities.
            // Move.
            Container.InstallElementAsSingle<PlayerIdleAbility>();
            Container.InstallElementAsSingle<PlayerMoveAbility>();
            Container.InstallElementAsSingle<PlayerFocusMoveAbility>();
            Container.InstallElementAsSingle<PlayerLookAtAbility>();

            // Attack.
            Container.InstallElementAsSingle<PlayerBaseAttackAbility>();
         
            // Specific.
            Container.InstallElementAsSingle<PlayerNoneAbility>();
            Container.InstallElementAsSingle<PlayerInteractAbility>();

            // Camera Abilities.
            Container.InstallElementAsSingle<CameraRotateAbility>();
        }

        private void InstallModels() 
        {
            Container.InstallModel<PlayerModel>();

            Container.InstallModel<CameraModel>();

            Container.InstallModel<QuestModel>();

            Container.InstallModel<MessageModel>();

            Container.InstallModel<AreaModel>();
        }

        private void InstallSignals() 
        {
          
        }
    }
}