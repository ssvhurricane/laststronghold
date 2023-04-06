using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class MainHUDView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Slider HelicopterHP, HelicopterPetrol;

        [SerializeField] protected Text ResourceMetal, ResourceStone, ResourceTree, ResourcePetrol;
        [SerializeField] protected Text TimeNextEarthQuake;

        [SerializeField] protected Image ImageMain, ImageRifle, ImageMG, ImaheRPG, ImageZoom;
        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }

        public void UpdateView(MainHUDViewArgs mainHUDViewArgs)
        {
            Id = mainHUDViewArgs.Id;

            HelicopterHP.value = mainHUDViewArgs.HelicopterHP;
            HelicopterPetrol.value = mainHUDViewArgs.HelicopterPetrol;

            ResourceMetal.text = mainHUDViewArgs.ResourceMetalCount.ToString();

            ResourceStone.text = mainHUDViewArgs.ResourceStoneCount.ToString();

            ResourceTree.text = mainHUDViewArgs.ResourceTreeCount.ToString();

            ResourcePetrol.text = mainHUDViewArgs.ResourcePetrolCount.ToString(); 

           // TimeNextEarthQuake
        }
    }

    public class MainHUDViewArgs : IWindowArgs 
    {
        public string Id { get; set; }

        public float HelicopterHP { get; set; }

        public float HelicopterPetrol { get; set; }

        public int ResourceMetalCount { get; set; }

        public int ResourceStoneCount { get; set; }

        public int ResourceTreeCount { get; set; }
        public int ResourcePetrolCount { get; set; }

        public string TimeNextEarthQuake { get; set; }

        public MainHUDViewArgs(){}

        public MainHUDViewArgs(string id, 
                                float helicopterHP, 
                                float helicopterPetrol, 
                                int resourceMetalCount,
                                int resourceStoneCount,
                                int resourceTreeCount,
                                int resourcePetrolCount,
                                string timeNextEarthQuake)
        {
            Id = id;

            HelicopterHP = helicopterHP;

            HelicopterPetrol  = helicopterPetrol;

            ResourceMetalCount = resourceMetalCount;
            ResourceStoneCount = resourceStoneCount;
            ResourceTreeCount = resourceTreeCount;
            ResourcePetrolCount = resourcePetrolCount;


            TimeNextEarthQuake = timeNextEarthQuake;

        }
    }
}