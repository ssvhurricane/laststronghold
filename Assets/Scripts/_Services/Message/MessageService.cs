using Services.Cheat;
using Services.Log;
using Zenject;

namespace Services.Message
{
    public class MessageService 
    {  
        private readonly SignalBus _signalBus;
       
        private readonly LogService _logService; 

        private readonly CheatService _cheatService;

        public MessageService(SignalBus signalBus,
                             LogService logService,
                             CheatService cheatService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _cheatService = cheatService;
        }
    }
}