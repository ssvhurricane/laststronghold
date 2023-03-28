using Zenject;

namespace Services.Area
{
    public class AreaService
    {
        private readonly SignalBus _signalBus;

        public AreaService(SignalBus signalBus)
        {
            // TODO:
            _signalBus = signalBus;
        }

        
    }
}
