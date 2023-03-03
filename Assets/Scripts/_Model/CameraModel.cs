using Data.Settings;
using Model.Inventory;
using Newtonsoft.Json;
using Services.Ability;
using UniRx;

namespace Model
{
    public class CameraModel : ILiveModel
    {
        [JsonProperty]
        public string Id { get; set; } = "CameraModel";

        [JsonProperty]
        public ModelType ModelType { get; set; }

        [JsonProperty]
        private ReactiveProperty<CameraAbilityContainer> _cameraAbilityContainer;

        [JsonIgnore]
        private readonly CameraServiceSettings[] _settings;

        [JsonIgnore]
        private CameraRotateAbility _cameraRotateAbility;

        [JsonIgnore]
        private IAbility _currentAbility;

        public CameraModel(CameraServiceSettings[] settings,
                           CameraRotateAbility cameraRotateAbility)
                         
        {
            _settings = settings;

            _cameraRotateAbility = cameraRotateAbility;

            //Init Base Ability.
            _cameraAbilityContainer = new ReactiveProperty<CameraAbilityContainer>();
            _cameraAbilityContainer.Value = new CameraAbilityContainer();

            _cameraAbilityContainer.Value.abilities.Add(_cameraRotateAbility);
        }
       
        public ReactiveProperty<CameraAbilityContainer> GetAbilityContainerAsReactive()
        {
            return _cameraAbilityContainer;
        }

        public IAbilityContainer GetAbilityContainer()
        {
            return _cameraAbilityContainer.Value;
        }

        public IAbility GetCurrentAbility()
        {
            return _currentAbility;
        }

        public IInventoryContainer GetInventoryContainer()
        {
            throw new System.NotImplementedException();
        }

        public void SetCurrentAbility(IAbility ability)
        {
            _currentAbility = ability;
        } 
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
