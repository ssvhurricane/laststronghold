using Model;
using Services.Cheat;
using Services.Log;
using Zenject;
using Signals;

namespace Services.Message
{
    public class MessageService 
    {  
        private readonly SignalBus _signalBus;
       
        private readonly LogService _logService; 

        private readonly CheatService _cheatService;
        private readonly MessageModel _messageModel;

        public MessageService(SignalBus signalBus,
                             LogService logService,
                             CheatService cheatService,
                             MessageModel messageModel)
        {
            _signalBus = signalBus;

            _logService = logService;

            _cheatService = cheatService;

            _messageModel = messageModel;

            _signalBus.Subscribe<MessageServiceSignals.NextMessage>(signal => OnNextMessage(signal.MessageOwnerName));
        }

        private void OnNextMessage(MessageOwnerName messageOwnerName)
        {
            ProcessingMessage();
        }

        public void ProcessingMessage()
        {
            // TODO:
        }
    }
}