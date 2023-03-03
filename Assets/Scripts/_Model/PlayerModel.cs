using Data.Settings;
using Model.Inventory;
using Newtonsoft.Json;
using Services.Ability;
using Services.Item;
using UniRx;

namespace Model
{
    public class PlayerModel : ILiveModel
    {
        [JsonProperty]
        public string Id { get; set; } = "PlayerModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }
        
        [JsonProperty]
        private ReactiveProperty<PlayerAbilityContainer> _playerAbilityContainer;

        [JsonProperty]
        private ReactiveProperty <PlayerInventoryContainer> _playerInventoryContainer;

        [JsonIgnore]
        private readonly PlayerSettings _settings;
       

        // Move section.
        [JsonIgnore]
        private PlayerIdleAbility _playerIdleAbility;

        [JsonIgnore]
        private PlayerMoveAbility _playerMoveAbility;

        [JsonIgnore]
        private PlayerFocusMoveAbility _playerFocusMoveAbility;

        [JsonIgnore]
        private PlayerLookAtAbility _playerLookAtAbility;

        // Attack section.
        [JsonIgnore]
        private PlayerBaseAttackAbility _playerAttackAbility;
        
        // Specific section.
        [JsonIgnore]
        private PlayerNoneAbility _playerNoneAbility;

        [JsonIgnore]
        private PlayerInteractAbility _playerInteractAbility;

        [JsonIgnore]
        private IAbility _currentAbility;

        [JsonIgnore]
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
            _playerAbilityContainer = new ReactiveProperty<PlayerAbilityContainer>();
            _playerAbilityContainer.Value = new PlayerAbilityContainer();
            
            _playerAbilityContainer.Value.abilities.Add(_playerIdleAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerMoveAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerFocusMoveAbility);
            _playerAbilityContainer.Value.abilities.Add(_playerLookAtAbility);

            _playerAbilityContainer.Value.abilities.Add(_playerAttackAbility);
            
            _playerAbilityContainer.Value.abilities.Add(_playerNoneAbility);
            
            _playerAbilityContainer.Value.abilities.Add(_playerInteractAbility);

            // Init Inventory.
            _playerInventoryContainer = new ReactiveProperty<PlayerInventoryContainer>();
            _playerInventoryContainer.Value = new PlayerInventoryContainer();
           
            _playerInventoryContainer.Value.Items.Add(_sniperRifleItem);
        }

        public ReactiveProperty<PlayerAbilityContainer> GetAbilityContainerAsReactive()
        {
            return _playerAbilityContainer;
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _playerAbilityContainer.Value;
        }

        public ReactiveProperty<PlayerInventoryContainer> GetInventoryContainerAsReactive()
        {
            return _playerInventoryContainer;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            return _playerInventoryContainer.Value;
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
