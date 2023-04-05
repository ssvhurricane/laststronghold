using System.Collections.Generic;

namespace Model
{
    public interface ILiveModel : IModel
    {
        public List<string> GetAbilities();

        public List<string> GetItems();
    }
}
