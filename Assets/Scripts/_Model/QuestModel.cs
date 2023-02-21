using Services.Quest;

namespace Model
{
    public class QuestModel : IModel
    {
        public string Id { get; set; }

        public ModelType ModelType { get; set; }

        private PlayerQuestContainer _playerQuestContainer;

        public QuestModel()
        {
             // Init quest Inventory.
            _playerQuestContainer = new PlayerQuestContainer();
            _playerQuestContainer.Initialize();
        }
        public IQuestContainer GetPlayerQuestContainer()
        {
            return _playerQuestContainer;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}
