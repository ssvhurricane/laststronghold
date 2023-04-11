namespace Signals
{
    public class MainHUDViewSignals
    {
        public class SelectWeaponItem 
        {
            public string Name { get; }
            public SelectWeaponItem(string name)
            {
                Name = name;
            }
        }
    }
}