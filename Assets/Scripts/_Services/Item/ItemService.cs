using Services.Essence;
using System.Collections.Generic;
using Zenject;

namespace Services.Item
{
    public class ItemService
    {
        private readonly SignalBus _signalBus;

        private List<BaseEssence> _itemViews, _itemWindows;
        public ItemService(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _itemViews = new List<BaseEssence>();
            _itemWindows = new List<BaseEssence>();
        }
       
        public void AddItemView(BaseEssence baseView)
        {
            _itemViews.Add(baseView);
        }

        public void RemoveItemView(BaseEssence baseView)
        {
            _itemViews.Remove(baseView);
        }

        public List<BaseEssence> GetAllItemViews()
        {
            return _itemViews;
        }
        public void ClearServiceValues() 
        {
            _itemViews.Clear();
            _itemWindows.Clear();
        }
    }
}
