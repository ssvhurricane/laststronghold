using Data.Settings;
using Services.Log;
using Zenject;

namespace Services.Interaction
{  
    public class InteractionService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly InteractionServiceSettings[] _interactionServiceSettings;

        public InteractionService( SignalBus signalBus, 
                                    LogService logService,
                                    InteractionServiceSettings[] interactionServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _interactionServiceSettings = interactionServiceSettings;
        }

        public void Interact(string interactId)
        {
            // TODO:
        }
    }
}
