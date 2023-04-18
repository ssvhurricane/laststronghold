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

        public class StartTimer
        {
            public string Id { get; }
            public float Rate { get; }

            public StartTimer(string id, float rate)
            {
                Id = id;
                
                Rate = rate;
            }
        }
    }
}