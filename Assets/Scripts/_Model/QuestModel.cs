using Newtonsoft.Json;
using Services.Quest;
using UniRx;

namespace Model
{
    public class QuestModel : IModel
    {
        [JsonProperty]
        public string Id { get; set; } = "QuestModel";

        [JsonProperty]
        public ModelType ModelType { get; set; }

        [JsonProperty]
        private ReactiveProperty<PlayerQuestContainer> _playerQuestContainer;

        public QuestModel()
        {
             // Init quest Inventory.
            _playerQuestContainer = new ReactiveProperty<PlayerQuestContainer>();
            _playerQuestContainer.Value = new PlayerQuestContainer();
        }

        public  ReactiveProperty<PlayerQuestContainer> GetPlayerQuestContainerAsReactive()
        {
            return _playerQuestContainer;
        }

        public IQuestContainer GetPlayerQuestContainer()
        {
            return _playerQuestContainer.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
