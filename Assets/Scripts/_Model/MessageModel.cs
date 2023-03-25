using Newtonsoft.Json;
using UniRx;
using Zenject;
using Data;
using Services.Message;

namespace Model
{
    public class MessageModel : IModel, ISerializableModel
    {
        [JsonProperty]
        public string Id { get; set; } = "MessageModel";
        
        [JsonProperty]
        public ModelType ModelType { get; set; }

        //Current ModelData.
        [JsonProperty]
        private ReactiveProperty<MessageSaveData> _projectData = new ReactiveProperty<MessageSaveData>();

        [JsonIgnore]
        private readonly SignalBus _signalBus;

        public MessageModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        } 

        public string SerializeModel(IModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public IModel DesirializeModel<TParam>(string model) where TParam : IModel
        {
            return JsonConvert.DeserializeObject<TParam>(model);
        }

        public void InitializeDafaultModelData()
        { 
            // TODO:
        }

        public void UpdateModelData(ISaveData saveData)
        { 
            // TODO:
        }

        public ReactiveProperty<MessageSaveData> GetMessageSaveDataAsReactive()
        {
            return _projectData;
        } 

        public MessageSaveData GetMessageSaveData()
        {
            return _projectData.Value;
        }
        
        public void Dispose()
        {
            // TODO:
        }
    }
}