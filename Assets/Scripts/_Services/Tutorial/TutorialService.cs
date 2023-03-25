using System.Collections.Generic;
using System.Linq;
using Constants;
using Data.Settings;
using Services.Cheat;
using Services.Log;
using Zenject;
using Signals;

namespace Services.Tutorial
{
    public class TutorialService 
    { 
        private readonly SignalBus _signalBus;
        private readonly TutorialServiceSettings[] _tutorialServiceSettings;
        private readonly LogService _logService; 

         private readonly CheatService _cheatService;

        public TutorialService(SignalBus signalBus,
                                TutorialServiceSettings[] tutorialServiceSettings,
                                LogService logService,
                                CheatService cheatService)
        { 
            _signalBus = signalBus;

            _tutorialServiceSettings = tutorialServiceSettings;

            _logService = logService;

            _cheatService = cheatService;

            AddCheats();
        }

        public Data.Settings.Tutorial GetCurrentTutorialData(int id)
        {
                return _tutorialServiceSettings.FirstOrDefault(tutor => tutor.Id ==  id.ToString()).Tutorial;
        }

        private void AddCheats()
        {
            /*
            var displayItems = _tutorialServiceSettings.Where().Select(new
            {

            });*/

            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "Tutor1",
                   "Tutor2"
               })
               .SetButtonName("Select tutorial and press call:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                          
                             _signalBus.Fire(new TutorialServiceSignals.Show(1));

                               break;
                           }

                        case 1: 
                           {
                          
                              _signalBus.Fire(new TutorialServiceSignals.Show(2));


                               break;
                           }
                      
                   }
               }), CheatServiceConstants.Tutorials);

        }
    }
}
