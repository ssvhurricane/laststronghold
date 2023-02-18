using Data.Settings;
using Model.Inventory;
using Services.Ability;
using Services.Item;
using Services.Quest;

namespace Model
{
    // TODO: save and serialize model.
    public class PlayerModel : ILiveModel
    {
        public string Id => _settings.Id;

        public ModelType ModelType { get; set; }

        private readonly PlayerSettings _settings;
        private PlayerAbilityContainer _playerAbilityContainer;
        private PlayerInventoryContainer _playerInventoryContainer;

        private PlayerQuestContainer _playerQuestContainer;

        // Move section.
        private PlayerIdleAbility _playerIdleAbility;
        private PlayerMoveAbility _playerMoveAbility;
        private PlayerFocusMoveAbility _playerFocusMoveAbility;
        private PlayerLookAtAbility _playerLookAtAbility;

        // Attack section.
        private PlayerBaseAttackAbility _playerAttackAbility;
        
        // Specific section.
        private PlayerNoneAbility _playerNoneAbility;

        private PlayerInteractAbility _playerInteractAbility;

        private IAbility _currentAbility;
      
        private SniperRifleItem _sniperRifleItem;

        public PlayerModel(PlayerSettings settings,
                PlayerIdleAbility playerIdleAbility,
                PlayerMoveAbility playerMoveAbility,
                PlayerFocusMoveAbility playerFocusMoveAbility,
                PlayerLookAtAbility playerLookAtAbility,
                PlayerBaseAttackAbility playerAttackAbility,
                PlayerNoneAbility playerNoneAbility,
                PlayerInteractAbility playerInteractAbility,
                SniperRifleItem sniperRifleItem
            )
        {
            _settings = settings;

            _playerIdleAbility = playerIdleAbility;
            _playerMoveAbility = playerMoveAbility;
            _playerFocusMoveAbility = playerFocusMoveAbility;
            _playerLookAtAbility = playerLookAtAbility;

            _playerAttackAbility = playerAttackAbility;
            
            _playerNoneAbility = playerNoneAbility;

            _playerInteractAbility = playerInteractAbility;

            _sniperRifleItem = sniperRifleItem;

            // Init Base Ability.
            _playerAbilityContainer = new PlayerAbilityContainer();
            
            _playerAbilityContainer.abilities.Add(_playerIdleAbility);
            _playerAbilityContainer.abilities.Add(_playerMoveAbility);
            _playerAbilityContainer.abilities.Add(_playerFocusMoveAbility);
            _playerAbilityContainer.abilities.Add(_playerLookAtAbility);

            _playerAbilityContainer.abilities.Add(_playerAttackAbility);
            
            _playerAbilityContainer.abilities.Add(_playerNoneAbility);
            
            _playerAbilityContainer.abilities.Add(_playerInteractAbility);

            // Init Inventory.
            _playerInventoryContainer = new PlayerInventoryContainer();
           
            _playerInventoryContainer.Items.Add(_sniperRifleItem);

            // Init quest Inventory.
            _playerQuestContainer = new PlayerQuestContainer();
            _playerQuestContainer.Initialize();
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _playerAbilityContainer;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return _playerInventoryContainer;
        }

        public IQuestContainer GetPlayerQuestContainer()
        {
            return _playerQuestContainer;
        }

        public void SetCurrentAbility(IAbility ability) 
        {
            _currentAbility = ability;
        }

        public IAbility GetCurrentAbility() 
        {
            return _currentAbility;
        }

        public void Dispose()
        {

        }
    }
}
