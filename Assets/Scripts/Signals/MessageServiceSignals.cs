using Services.Message;

namespace Signals
{
    public class MessageServiceSignals
    {
        public class NextMessage
        {
            public MessageOwnerName MessageOwnerName { get; set; }
            public NextMessage(MessageOwnerName messageOwnerName)
            {
                MessageOwnerName = messageOwnerName;
            }
        }

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