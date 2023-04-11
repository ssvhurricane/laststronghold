using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
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

        [SerializeField] protected Image ImageMainInteract, ImageMainShoot, ImageOne, ImageTwo, ImageThree, ImageZoom;

        [SerializeField] protected OnScreenButton ButtonImageOne, ButtonImageTwo, ButtonImageThree;

        private SignalBus _signalBus;

        private Image _currentSelectItem;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));

           // ButtonImageOne.
        }

        public void UpdateView(MainHUDViewHeliCopterContainerArgs mainHUDViewHeliCopterContainerArgs)
        {
            // TODO:
        }

        public void UpdateView(MainHUDViewInteractionContainerArgs mainHUDViewInteractionContainerArgs)
        {
           if(!mainHUDViewInteractionContainerArgs.IsReadyInteraction)
           {  
               ImageMainInteract.gameObject.SetActive(true);

               ImageMainShoot.gameObject.SetActive(false);
           }
           else
           {
               ImageMainInteract.gameObject.SetActive(false);

               ImageMainShoot.gameObject.SetActive(true);
           }

            ImageOne.sprite = mainHUDViewInteractionContainerArgs.ImageOne;

            ImageTwo.sprite = mainHUDViewInteractionContainerArgs.ImageTwo;

            ImageThree.sprite = mainHUDViewInteractionContainerArgs.ImageThree;
/*
            if(!mainHUDViewInteractionContainerArgs.ImageOneIsActive)
                ButtonImageOne.interactable = false;
            else
                 ButtonImageOne.interactable = true;

            if(!mainHUDViewInteractionContainerArgs.ImageTwoIsActive)
                ButtonImageTwo.interactable = false;
            else
                 ButtonImageTwo.interactable = true;

             if(!mainHUDViewInteractionContainerArgs.ImageThreeIsActive)
                ButtonImageThree.interactable = false;
            else
                 ButtonImageThree.interactable = true;*/
        }

        public void UpdateView(MainHUDViewResourcesContainerArgs mainHUDViewResourcesContainerArgs)
        {
            // TODO:
        }
    }

    public class MainHUDViewHeliCopterContainerArgs : IWindowArgs 
    {
        public string Id { get; set; }

        public float HelicopterHP { get; set; }

        public float HelicopterPetrol { get; set; }

        public MainHUDViewHeliCopterContainerArgs(){}

        public MainHUDViewHeliCopterContainerArgs(string id, 
                                float helicopterHP, 
                                float helicopterPetrol)
        {
            Id = id;

            HelicopterHP = helicopterHP;

            HelicopterPetrol  = helicopterPetrol;
        }
    }

    public class MainHUDViewInteractionContainerArgs : IWindowArgs 
    {
        public string Id { get; set; }

        public bool IsReadyInteraction { get; set; }

        public bool ImageOneIsActive { get; set; } = true;

        public Sprite ImageOne { get; set; }

        public bool ImageTwoIsActive { get; set ; } = true;

        public Sprite ImageTwo { get; set; }

        public bool ImageThreeIsActive { get; set ;} = true;
        public Sprite ImageThree { get; set; }
      
        public MainHUDViewInteractionContainerArgs(){}

        public MainHUDViewInteractionContainerArgs(string id, bool isReadyInteraction, Sprite imageOne, Sprite imageTwo, Sprite imageThree)
        {
            Id = id;

            IsReadyInteraction = isReadyInteraction;

            ImageOne = imageOne;

            ImageTwo = imageTwo;

            ImageThree  = imageThree;
        }
    }

    public class MainHUDViewResourcesContainerArgs : IWindowArgs 
    {
        public string Id { get; set; }
      
        public MainHUDViewResourcesContainerArgs(){}

        public MainHUDViewResourcesContainerArgs(string id)
        {
            Id = id;
        }
    }
}