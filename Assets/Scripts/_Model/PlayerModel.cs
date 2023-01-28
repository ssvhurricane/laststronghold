using Data.Settings;
using Model.Inventory;
using Services.Ability;
using Services.Item;

namespace Model
{
    public class PlayerModel : ILiveModel
    {
        public string Id => _settings.Id;

        public ModelType ModelType { get; set; }

        private readonly PlayerSettings _settings;
        private PlayerAbilityContainer _playerAbilityContainer;
        private PlayerInventoryContainer _playerInventoryContainer;

        private PlayerIdleAbility _playerIdleAbility;
        private PlayerMoveAbility _playerMoveAbility;
        private PlayerLookAtAbility _playerLookAtAbility;
        private PlayerBaseAttackAbility _playerAttackAbility;
        
        private PlayerNoneAbility _playerNoneCurrentAbility;

        private IAbility _currentAbility;
      
        private SniperRifleItem _sniperRifleItem;

        public PlayerModel(PlayerSettings settings,
                PlayerIdleAbility playerIdleAbility,
                PlayerMoveAbility playerMoveAbility,
                PlayerLookAtAbility playerLookAtAbility,
                PlayerBaseAttackAbility playerAttackAbility,
                PlayerNoneAbility playerNoneCurrentAbility,
                SniperRifleItem sniperRifleItem
            )
        {
            _settings = settings;

            _playerIdleAbility = playerIdleAbility;
            _playerMoveAbility = playerMoveAbility;
            _playerLookAtAbility = playerLookAtAbility;

            _playerAttackAbility = playerAttackAbility;
            
            _playerNoneCurrentAbility = playerNoneCurrentAbility;

            _sniperRifleItem = sniperRifleItem;

           //Init Base Ability.
            _playerAbilityContainer = new PlayerAbilityContainer();
            
            _playerAbilityContainer.abilities.Add(_playerIdleAbility);
            _playerAbilityContainer.abilities.Add(_playerMoveAbility);
            _playerAbilityContainer.abilities.Add(_playerLookAtAbility);

            _playerAbilityContainer.abilities.Add(_playerAttackAbility);
            
            _playerAbilityContainer.abilities.Add(_playerNoneCurrentAbility);
          
            // Init Inventory.
            _playerInventoryContainer = new PlayerInventoryContainer();
           
            _playerInventoryContainer.Items.Add(_sniperRifleItem);
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _playerAbilityContainer;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return _playerInventoryContainer;
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
