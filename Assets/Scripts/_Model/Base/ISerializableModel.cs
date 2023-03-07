using Data;

namespace Model
{
    public interface ISerializableModel
    {
            public string SerializeModel(IModel model);
        
            public IModel DesirializeModel<TParam>(string model) where TParam : IModel;
            
            public void InitializeDafaultModelData();

            public void UpdateModelData(ISaveData saveData);
    }
}
