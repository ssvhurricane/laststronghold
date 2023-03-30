using System.Collections.Generic;
using Constants;
using Model;
using Services.Cheat;
using Zenject;

namespace Services.Area
{
    public class AreaService
    {
        private readonly SignalBus _signalBus;

        private readonly CheatService _cheatService;
        private readonly AreaModel _areaModel;
        
        public AreaService(SignalBus signalBus,
                         CheatService cheatService,
                         AreaModel areaModel)
        {
            _signalBus = signalBus;

            _cheatService = cheatService;

            _areaModel = areaModel;

            AddCheats();
        }

        public void AreaProcessing()
        {
            // TODO:
        }

        private void AddCheats()
        {
           //1. Построить Область.

            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("Build Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

           //2. Скрыть Область.
           _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("Hide Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

           //3. Поазать область.
           _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("Show Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

            //4. Прокачать область.
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("LevelUp Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

            //5. Разрушить область.
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("Destroy Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

            //6. Востановить область.
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("ReBuild Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

            //7. Получить область как ресурс.
            _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "PoliceOffice"
               })
               .SetButtonName("Get As Resource Selected Area:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                           
                                // TODO:
                               break;
                           }
                   }
               }), CheatServiceConstants.Areas);

            //8. Скрыть все области.
            _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Hide All Areas")
            .SetButtonCallback(() =>
            {/*
                foreach (var quest in _quests)
                {
                    ForceCompleteQuest(quest);
                }*/
            }), CheatServiceConstants.Areas);
        }
    }
}
