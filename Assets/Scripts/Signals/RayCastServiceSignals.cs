using Services.RayCast;

namespace Signals
{
    public class RayCastServiceSignals
    {
        public class AddReceiver
        {
            public ReceiverHolder ReceiverHolder { get; }

            public AddReceiver(ReceiverHolder receiverHolder)
            {
                ReceiverHolder = receiverHolder;
            }
        }

        public class AddTransmitter
        {
            public TransmitterHolder TransmitterHolder { get; }

            public AddTransmitter(TransmitterHolder transmitterHolder)
            {
                TransmitterHolder = transmitterHolder;
            }
        }
    }
}
