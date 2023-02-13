using System.Collections.Generic;
using Services.Area;
using Services.Essence;
using Services.Item;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View
{
    public class SuperMarketView : NetworkEssence, IBuilding
    {
        [SerializeField] protected Canvas CanavasDesc;
        [SerializeField] protected Text TextDesc;
        [SerializeField] protected GameObject Prefab;
        
        private SignalBus _signalBus;

        public int Level { get; set; }
        public StatusType StatusType { get; set; }
        public StageType StageType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInteractive { get; set; }
        public AreaType AreaType { get; set; }
        public List<IItem> Items { get; set; }
        public List<IView> ViewWorkers { get; set; }
        public int Purse { get; set; }
        int IArea.Id { get; set; }

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public int GetLevel()
        {
            return -1;
        }

        public bool LevelUp()
        {
            return false;
        }

        public void SetLevel()
        {
           // TODO:
        }
    }
}
