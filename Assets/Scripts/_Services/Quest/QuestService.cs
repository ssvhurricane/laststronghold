using System.Collections.Generic;
using Data.Settings;
using Zenject;
using Model;
using System.Linq;
using Services.Log;
using Services.Cheat;
using Signals;
using Constants;

namespace Services.Quest
{
    public class QuestService
    {
        private readonly SignalBus _signalBus;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private readonly LogService _logService; 
        private readonly CheatService _cheatService;
        private readonly QuestModel _questModel;

        private Flow _flow;

        private Dictionary<int, List<Data.Settings.Quest>> _questsThreads;
        public QuestService(SignalBus signalBus, 
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            LogService logService,
                            CheatService cheatService,
                            QuestModel questModel)
        {
            _signalBus = signalBus;

            _questServiceSettings = questServiceSettings;

            _questsSettings = questsSettings;

            _logService = logService;

            _cheatService = cheatService;

            _questModel = questModel;
           
            _questsThreads = new Dictionary<int, List<Data.Settings.Quest>>();

            LoadRegistryData();

            AddCheats();

            _signalBus.Subscribe<QuestServiceSignals.OnQuestActivateEvent>(signal => OnQuestActivate(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestAssignEvent>(signal => OnQuestAssign(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestBuildEvent>(signal => OnQuestBuild(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestCollectEvent>(signal => OnQuestCollect(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestDestroyEvent>(signal => OnQuestDestroy(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestGetEvent>(signal => OnQuestGet(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestKillEvent>(signal => OnQuestKill(signal.Id));
            _signalBus.Subscribe<QuestServiceSignals.OnQuestUpgradeEvent>(signal => OnQuestUpgrade(signal.Id));
        }

        private void OnQuestActivate(int id)
        {
            ProcessingQuest(id);
        }
        private void OnQuestAssign(int id)
        {
            ProcessingQuest(id);
        } 
        private void OnQuestBuild(int id)
        {
            ProcessingQuest(id);
        }
        private void OnQuestCollect(int id)
        {
            ProcessingQuest(id);
        }
        private void OnQuestDestroy(int id)
        {
            ProcessingQuest(id);
        }
        private void OnQuestGet(int id)
        {
            ProcessingQuest(id);
        }
        private void OnQuestKill(int id)
        {
            ProcessingQuest(id);
        }  
        private void OnQuestUpgrade(int id)
        {
            ProcessingQuest(id);
        }

        private void ProcessingQuest(int id)
        {
            var savedQuest = _questModel.GetQuestSaveData().QuestItemDatas.FirstOrDefault(quest => quest.Id == id);

            if (savedQuest == null) return;
        
            // Refresh quest model.
            var qModelData = _questModel.GetQuestSaveData();

            if(savedQuest.QuestState == QuestState.Active)
            {
                var activeQuest = qModelData.QuestItemDatas.FirstOrDefault(itemData => itemData.Id == savedQuest.Id);

                ++activeQuest.CurrentValue;

                if(activeQuest.CurrentValue == _questsSettings.FirstOrDefault(item => item.Quest.Id == savedQuest.Id).Quest.NeedValue)
                    activeQuest.QuestState = QuestState.Complete;
                
                _questModel.UpdateModelData(qModelData);
            }   

           NextQuestInFlow();
        }

        public void InitializeFlow(Flow flow)
        {
            _flow = flow;
            _flow.Parse();

            var savedQuests = _questModel.GetQuestSaveData().QuestItemDatas.Where(quest => _flow.ParseThreads.Any(item => item == quest.ThreadId));

            if (savedQuests == null && savedQuests.Count() == 0) return;

            switch(_flow.FlowExecutionMode)
            {
                case FlowExecutionMode.Consistent:
                {
                    var qModelData = _questModel.GetQuestSaveData();
                       
                    if(qModelData.QuestItemDatas.FirstOrDefault().QuestState == QuestState.Inactive)
                        qModelData.QuestItemDatas.FirstOrDefault().QuestState = QuestState.Active;
                  
                    _questModel.UpdateModelData(qModelData);

                    break;
                }
                
                case FlowExecutionMode.Parallel:
                {  
                    var qModelData = _questModel.GetQuestSaveData();

                    foreach(var savedQuest in savedQuests)
                    {
                        if(savedQuest.QuestState == QuestState.Inactive)
                          qModelData.QuestItemDatas.FirstOrDefault(itemData => itemData.Id == savedQuest.Id).QuestState = QuestState.Active;
                    }

                    _questModel.UpdateModelData(qModelData);

                    break;
                }
            }
        }

        private void NextQuestInFlow()
        { 
            var savedQuests = _questModel.GetQuestSaveData().QuestItemDatas.Where(quest => _flow.ParseThreads.Any(item => item == quest.ThreadId));

            if (savedQuests == null && savedQuests.Count() == 0) return;

            if (savedQuests.All(item => item.QuestState == QuestState.Complete))
            {
                    _logService.ShowLog(GetType().Name,
                                Services.Log.LogType.Message,
                                "All quests in flow complete!",
                                LogOutputLocationType.Console);
                    //new Event FlowComplete(id)...
                    return;
            }

            switch(_flow.FlowExecutionMode)
            {
                case FlowExecutionMode.Consistent:
                {
                    var qModelData = _questModel.GetQuestSaveData();
                       
                    // TODO:
                    //_questModel.UpdateModelData(qModelData);

                    break;
                }
                
                case FlowExecutionMode.Parallel:
                {  
                    var qModelData = _questModel.GetQuestSaveData();

                    foreach(var savedQuest in savedQuests)
                    {
                        // TODO:
                    }

                   // _questModel.UpdateModelData(qModelData);

                    break;
                }
            }
        }

        private Dictionary<int, List<Data.Settings.Quest>> GetAllQuests()
        {
            return _questsThreads;
        }

        private Data.Settings.Quest GetNextQuestInThread(int threadId, Data.Settings.Quest currentQuest)
         {
            var threadQuests = GetThreadByKey(threadId);

            int index = 0;
            foreach (var quest in threadQuests.Value) 
            {
                if (quest.Id == currentQuest.Id)
                {
                     index = threadQuests.Value.IndexOf(quest);

                    ++index;

                    break;
                }
            }

            return index == threadQuests.Value.Count ? null : threadQuests.Value[index];
         }

        private KeyValuePair<int, List<Data.Settings.Quest>> GetThreadByKey(int key)
         {
            return _questsThreads.FirstOrDefault(item => item.Key == key);
         }

        private void LoadRegistryData()
         {
            foreach(var data in _questsSettings)
            {
                if(data.Quest.ThreadId != 0)
                {
                    if(!_questsThreads.ContainsKey(data.Quest.ThreadId))
                         _questsThreads[data.Quest.ThreadId] = new List<Data.Settings.Quest>();
                    
                    _questsThreads[data.Quest.ThreadId].Add(data.Quest);
                }
            }
         }

        private void AddCheats()
          {
                _cheatService.AddCheatItemControl<CheatButtonDropdownControl>(button => button
               .SetDropdownOptions(new List<string>() 
               {
                   "Activate",
                   "Get",
                   "Collect",
                   "Build",
                   "Destroy",
                   "Kill",
                   "Assign",
                   "Upgrade"
               })
               .SetButtonName("Select Quest Event and press call:")
               .SetButtonCallback(() =>
               {
                   switch (button.CurIndex) 
                   {
                       case 0: 
                           {
                            // TODO:

                             _signalBus.Fire(new QuestServiceSignals.OnQuestActivateEvent(1));

                              _signalBus.Fire(new QuestServiceSignals.OnQuestActivateEvent(3));

                             _signalBus.Fire(new QuestServiceSignals.OnQuestActivateEvent(2));

                               break;
                           }
                       case 1: 
                           {
                                _signalBus.Fire(new QuestServiceSignals.OnQuestGetEvent(1));
                               break;
                           }
                       case 2:
                           {
                                _signalBus.Fire(new QuestServiceSignals.OnQuestCollectEvent(1));

                               break;
                           }
                       case 3:
                           {
                               _signalBus.Fire(new QuestServiceSignals.OnQuestBuildEvent(1));

                            break; 
                           }
                       case 4: 
                           {
                               _signalBus.Fire(new QuestServiceSignals.OnQuestDestroyEvent(1));
                               break;
                           }
                       case 5: 
                           {
                               _signalBus.Fire(new QuestServiceSignals.OnQuestKillEvent(1));
                               break; 
                           }
                       case 6: 
                           {
                               _signalBus.Fire(new QuestServiceSignals.OnQuestAssignEvent(1));

                               break;
                           }
                       case 7:
                           {
                               _signalBus.Fire(new QuestServiceSignals.OnQuestUpgradeEvent(1));

                               break;
                           }
                   }
               }), CheatServiceConstants.Quests);

            _cheatService.AddCheatItemControl<CheatButtonControl>(button => button
             .SetButtonName("Complete Quests")
            .SetButtonCallback(() =>
            {/*
                foreach (var quest in _quests)
                {
                    ForceCompleteQuest(quest);
                }*/
            }), CheatServiceConstants.Quests);
          }
    }
}