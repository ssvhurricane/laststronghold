using Services.Pool;
using Zenject;

namespace Services.SFX
{
    public class SFXService 
    {
       private readonly SignalBus _signalBus;

       private readonly PoolService _poolService;
       public SFXService(SignalBus signalBus, PoolService poolService) 
        {
            _signalBus = signalBus;

            _poolService = poolService;
        }

        public void Play(ISFXItem iSFXItem)
        {
            // TODO:
        }

        public void Stop(ISFXItem iSFXItem)
        {
            // TODO:
        }
    }
}
