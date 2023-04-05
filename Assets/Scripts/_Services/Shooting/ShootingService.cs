using Services.Log;
using Zenject;

namespace Services.Shooting
{
    public class ShootingService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        public ShootingService(SignalBus signalBus, LogService logService)
        {
            _signalBus = signalBus;

            _logService = logService;
        }
    }
}
