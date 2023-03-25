
using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Project;
using Services.Tutorial;
using Services.Window;
using Zenject;

namespace Presenters.Window
{
    public class MessagePresenter
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly TutorialService _tutorialService;

        private readonly ProjectService _projectService;

        public MessagePresenter(SignalBus signalBus,
                                LogService logService,
                                IWindowService windowService,
                                FactoryService factoryService,
                                HolderService holderService,
                                TutorialService tutorialService,
                                ProjectService projectService)
        {
            _signalBus = signalBus;

            _logService = logService;

            _windowService = windowService;

            _factoryService = factoryService;

            _holderService = holderService;

            _tutorialService = tutorialService;
        }

        public void ShowView()
        {

            // TODO//
        }
    }
}
