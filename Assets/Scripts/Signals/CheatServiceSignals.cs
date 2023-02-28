namespace Signals
{
    public class CheatServiceSignals
    {
        public class ActivateCheatItemView
        {
            public string Name { get; set;}
            public ActivateCheatItemView(string name)
            {
                Name = name;
            }
        }
    }
}