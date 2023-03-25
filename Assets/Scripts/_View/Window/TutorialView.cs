using Data.Settings;
using Services.Localization;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/TutorialView", WindowType.PopUpWindow)]
    public class TutorialView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Text Description;
        [SerializeField] protected Image StepImage;
        [SerializeField] protected Button LeftArrowButton;
        [SerializeField] protected Button RightArrowButton;
        [SerializeField] protected Text StepDescription;
        [SerializeField] public Button BackToGameButton;

        private SignalBus _signalBus;
        private  LocalizationService _localizationService;

        [Inject]
        public void Constrcut(SignalBus signalBus,
        LocalizationService localizationService)
        {
            _signalBus = signalBus;

            _localizationService = localizationService;
        }

        public void UpdateView(TutorialViewArgs tutorialViewArgs)
        {
            if (_localizationService.HaveKey(tutorialViewArgs.Tutorial.Description))
                    Description.text = _localizationService.Translate(tutorialViewArgs.Tutorial.Description);
        }
    }

    public class TutorialViewArgs : IWindowArgs
    {
        public Tutorial Tutorial { get; set; }

        public TutorialViewArgs(){}
        
        public TutorialViewArgs(Tutorial tutorial)
        {
            Tutorial = tutorial;
        }
    }
}