using System;
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

            _cheatItems = new Dictionary<string, List<CheatItemControlData>>();
        }

        public Dictionary<string, List<CheatItemControlData>> CheatItemControlProcessing()
        {
            // TODO:
            foreach(var cheatItemData in _cheatServiceSettings.CheatItems)
            {
                if(_cheatItems.ContainsKey(cheatItemData.Name)) continue;

                _cheatItems.Add(cheatItemData.Name, new List<CheatItemControlData>(){});
            }
            return _cheatItems;
        }

        public void AddCheatItemControl<TParam>(Action<TParam> initer) where TParam : CheatItemControl
        {

        }

        public void AddCheatItemPopUp<TParam>() where TParam : CheatItemControl
        {
            
        }
    }
}
