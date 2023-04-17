using Services.Essence;

namespace Signals
{
    public class MainHUDViewSignals
    {
        public class SelectWeaponItem 
        {
            public IEssence Essence { get; }
            public SelectWeaponItem(IEssence essence)
            {
                Essence = essence;
            }
        }
    }
}