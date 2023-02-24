using System.Collections.Generic;
using Data.Settings;
using Zenject;

namespace Services.Cheat
{
    public class CheatService 
    {
        private readonly SignalBus _signalBus;

        private readonly CheatServiceSettings _cheatServiceSettings;

        private Dictionary<string, List<CheatItemControlData>> _cheatItems;
        public CheatService(SignalBus signalBus,
                            CheatServiceSettings cheatServiceSettings)
        {
            _signalBus = signalBus;

            _cheatServiceSettings = cheatServiceSettings;
        }

        public Dictionary<string, List<CheatItemControlData>> CheatItemControlProcessing()
        {
            // TODO:
            return null;
        }
    }


}
