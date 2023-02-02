using Zenject;

namespace Services.Building
{
    public class BuildingService
    {
        private readonly SignalBus _signalBus;

        public BuildingService(SignalBus signalBus)
        {
            // TODO:
            _signalBus = signalBus;
        }
    }
}
