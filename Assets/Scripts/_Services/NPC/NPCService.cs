using Zenject;

namespace Services.NPC
{
    public class NPCService
    {
        private readonly SignalBus _signalBus;

        public NPCService(SignalBus signalBus)
        {
            _signalBus = signalBus;
            // TODO:
        }
    }
}