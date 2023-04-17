using Services.Pool;
using Zenject;

namespace Services.VFX
{
    public class VFXService
    {
        private readonly SignalBus _signalBus;
        private readonly PoolService _poolservice;
        public VFXService(SignalBus signalBus, PoolService poolService) 
        {
            _signalBus = signalBus;

            _poolservice = poolService;
        }

        public void Play(IVFXItem iVFXItem)
        {
            // TODO:
        }

        public void Stop(IVFXItem iVFXItem)
        {
            // TODO:
        }
    }
}