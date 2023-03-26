namespace Signals
{
    public class MessageServiceSignals
    {
        public class ActivateMessageItemView
            {
                public string Name { get; set;}
                public ActivateMessageItemView(string name)
                {
                    Name = name;
                }
            }
    }
}