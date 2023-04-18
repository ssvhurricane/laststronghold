using Signals;
using Services.Project;
using Services.Resources;
using Services.Scene;
using Services.Window;
using Utils.Helpers;
using Zenject;
using Presenters;
using Services.Essence;
using Model;
using Services.Factory;
using Presenters.Window;
using Services.Anchor;
using Services.Pool;
using Services.Log;
using Services.Localization;
using Services.SaveData;
using Services.Cheat;

namespace Bootstrap
{
    public class CoreInstaller : Installer
    {
        public override void InstallBindings()
        { 
            InstallModels();

            BindPresenters();

            BindServices();

            InstallSignals();
           
            InstallAnimations();

            InitExecutionOrder();

            InstallPools();
        }

        private void BindPresenters()
        { 
            Container.InstallElementAsSingle<MainMenuPresenter>();

            Container.Bind<IInitializable>().To<ProjectPresenter>().AsSingle(); 

            Container.InstallElementAsSingle<GameSettingsPresenter>();
        }
        void InitExecutionOrder() { }
       
        private void BindServices()
        { 
            Container.InstallElementAsSingle<SaveDataService>();

            Container.InstallElementAsSingle<ProjectService>();

            Container.Bind<ISceneService>().To<SceneService>().AsSingle();
            
            Container.InstallElementAsSingle<EssenceService>();

            Container.Bind<IWindowService>().To<WindowService>().AsSingle();

            Container.InstallElementAsSingle<ResourcesService>();
           
            Container.InstallElementAsSingle<HolderService>();

            Container.InstallElementAsSingle<AnchorService>(); 
            
            Container.InstallElementAsSingle<FactoryService>();

            Container.InstallElementAsSingle<PoolService>();

            Container.InstallElementAsSingle<LogService>();

            Container.InstallElementAsSingle<LocalizationService>(); 

            Container.InstallElementAsSingle<CheatService>();
        }
      
        private void InstallAnimations()
        {
            Container
                .BindInterfacesAndSelfTo<FadeAndScaleWindowAnimation>()
                .AsTransient();
        }
        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            // Scene Service Signals.
            Container.DeclareSignal<SceneServiceSignals.SceneLoadingStarted>();
            Container.DeclareSignal<SceneServiceSignals.SceneLoadingCompleted>();
            Container.DeclareSignal<SceneServiceSignals.SceneUnloadingCompleted>();

            // Service Signals.
            Container.DeclareSignal<EssenceServiceSignals.Register>();
            Container.DeclareSignal<EssenceServiceSignals.AddHolder>();
            Container.DeclareSignal<EssenceServiceSignals.Shown>().OptionalSubscriber();
            Container.DeclareSignal<EssenceServiceSignals.Hidden>().OptionalSubscriber();

            // Window Service Signals.
            Container.DeclareSignal<WindowServiceSignals.Register>();
            Container.DeclareSignal<WindowServiceSignals.AddHolder>();
            Container.DeclareSignal<WindowServiceSignals.Shown>().OptionalSubscriber();
            Container.DeclareSignal<WindowServiceSignals.Hidden>().OptionalSubscriber();

            // Anchor Service Signals.
            Container.DeclareSignal<AnchorServiceSignals.Add>();
            Container.DeclareSignal<AnchorServiceSignals.Activate>();
            Container.DeclareSignal<AnchorServiceSignals.Deactivate>();

            // RayCast Service Signals.
            Container.DeclareSignal<RayCastServiceSignals.AddReceiver>();
            Container.DeclareSignal<RayCastServiceSignals.AddTransmitter>();

            // Quest Service Signals.
            Container.DeclareSignal<QuestServiceSignals.OnQuestActivateEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestAssignEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestBaseEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestBuildEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestCollectEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestDestroyEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestGetEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestKillEvent>();
            Container.DeclareSignal<QuestServiceSignals.OnQuestUpgradeEvent>();
            Container.DeclareSignal<QuestServiceSignals.ActivateQuestMenuFlowView>();

            // Cheats Signals.
            Container.DeclareSignal<CheatServiceSignals.ActivateCheatItemView>();

            Container.DeclareSignal<MessageServiceSignals.NextMessage>();
            Container.DeclareSignal<MessageServiceSignals.ActivateMessageItemView>();

            // GameSettingsView.
            Container.DeclareSignal<GameSettingsViewSignals.Apply>();

            Container.DeclareSignal<MainHUDViewSignals.SelectWeaponItem>();
            Container.DeclareSignal<MainHUDViewSignals.StartTimer>();
            
            // TutorialView.
            Container.DeclareSignal<TutorialServiceSignals.Show>();
  
            Container.DeclareSignal<ShootingServiceSignals.Hit>();
        }

       public void InstallModels()
        {
            Container.InstallModel<ProjectModel>();
        }

        private void InstallPools()
        {
            Container.InstallElementAsSingle<ViewPool>();
        }
    }
}