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

            CheatItemControlProcessing();
        }

        private Dictionary<string, List<CheatItemControlData>> CheatItemControlProcessing()
        {
            foreach(var cheatItemData in _cheatServiceSettings.CheatItems)
            {
                if(_cheatItems.ContainsKey(cheatItemData.Name)) continue;

                _cheatItems.Add(cheatItemData.Name, new List<CheatItemControlData>(){});
            }
            return _cheatItems;
        }

        public Dictionary<string, List<CheatItemControlData>> GetCheatItems()
        {
            return _cheatItems;
        }

        public void AddCheatItemControl<TParam>(Action<TParam> initer, string cheatItemName) where TParam : CheatItemControl
        {
            if(_cheatItems != null && _cheatItems.Count != 0 && _cheatItems.ContainsKey(cheatItemName))
            {
                // TODO:
                var data = new CheatItemControlData()
                {
                    Id = cheatItemName,
                    Name = cheatItemName,
                    CheatItemType = typeof(TParam),
                    CheatAction =  null
                };

                _cheatItems[cheatItemName].Add(data);
            }
        }

        public void AddCheatItemPopUp<TParam>() where TParam : CheatItemControl
        {
            
        }
    }
}
