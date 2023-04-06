using System.Linq;
using Data.Settings;
using Services.Log;
using Zenject;

namespace Services.Shooting
{
    public class ShootingService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly ShootingServiceSettings[] _shootingServiceSettings;

        public ShootingService(SignalBus signalBus, 
                                LogService logService,
                                ShootingServiceSettings[] shootingServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _shootingServiceSettings = shootingServiceSettings;
        }

        public void Shoot(string shootElementId)
        {
            var shootElementSettings = _shootingServiceSettings.FirstOrDefault(element => element.Id == shootElementId);

            // TODO:
        }
    }
}
