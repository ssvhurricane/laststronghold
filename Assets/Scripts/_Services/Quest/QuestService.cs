using System.Collections.Generic;
using Data.Settings;
using Zenject;
using Model;
using System.Linq;
using Services.Log;
using Services.Cheat;
using static Signals.QuestServiceSignals;
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

        private QuestActivateCondition _questActivateCondition;
        private QuestAssignConditon _questAssignConditon;
        private QuestBuildCondition _questBuildCondition;
        private QuestCollectCondition _questCollectCondition;
        private QuestDestroyCondition _questDestroyCondition;
        private QuestGetCondition _questGetCondition;
        private QuestKillCondition _questKillCondition;
        private QuestUpgradeCondition _questUpgradeCondition;

        private Flow _flow;

        private Dictionary<int, List<Data.Settings.Quest>> _questsThreads;
        public QuestService(SignalBus signalBus, 
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            LogService logService,
                            CheatService cheatService,
                            QuestModel questModel,
                            QuestActivateCondition questActivateCondition,
                            QuestAssignConditon questAssignConditon,
                            QuestBuildCondition questBuildCondition,
                            QuestCollectCondition questCollectCondition,
                            QuestDestroyCondition questDestroyCondition,
                            QuestGetCondition questGetCondition,
                            QuestKillCondition questKillCondition,
                            QuestUpgradeCondition questUpgradeCondition)
        {
            _signalBus = signalBus;

            _questServiceSettings = questServiceSettings;

            _questsSettings = questsSettings;

            _logService = logService;

            _cheatService = cheatService;

            _questModel = questModel;

            _questActivateCondition = questActivateCondition;
            _questAssignConditon = questAssignConditon;
            _questBuildCondition = questBuildCondition;
            _questCollectCondition = questCollectCondition;
            _questDestroyCondition = questDestroyCondition;
            _questGetCondition = questGetCondition;
            _questKillCondition = questKillCondition;
            _questUpgradeCondition = questUpgradeCondition;
           
            _questsThreads = new Dictionary<int, List<Data.Settings.Quest>>();

            LoadRegistryData();
        }

        public void InitializeFlow(Flow flow)
        {
            _flow = flow;
            _flow.Parse();

            var savedQuests = _questModel.GetQuestSaveData().QuestItemDatas;

            if (savedQuests == null && savedQuests.Count() == 0) return;

            switch(_flow.FlowExecutionMode)
            {
                case FlowExecutionMode.Consistent:
                {
                    // TODO:
                    break;
                }
                case FlowExecutionMode.Parallel:
                {  
                    // Refresh quest model.
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

           //ProcessingQuests(_questModel);
/*
            // Load saved quests.
            if(savedQuests != null && savedQuests.Count > 0)
                foreach(var savedQuest in savedQuests)  AddQuestToList(GetQuestById(savedQuest.Id));
                       
            
            else
            {
                // Create new quests.
                if(flow.ParseThreads != null && flow.ParseThreads.Count != 0)
                {
                    foreach(var thread in flow.ParseThreads)
                    {
                        var quest = GetThreadByKey(thread).Value?.FirstOrDefault();

                        if (quest != null) 
                        { 
                          //  if(!string.IsNullOrEmpty(quest.Tag) || !string.IsNullOrEmpty(quest.Value))
                                _questModel.GetPlayerQuestContainer().SaveQuest(AddQuestToList(quest));
                        }
                    }

                    //new OnQuestInitializeFlowEvent(_quests).Invoke();
                }
            }*/

        }

        private void ProcessingQuests(QuestModel questModel)
        {
            var savedQuests = _questModel.GetQuestSaveData().QuestItemDatas;

            foreach(var savedQuest in savedQuests)
            {
                if(savedQuest.QuestState == QuestState.Active)
                {
                    switch(_questsSettings.FirstOrDefault(quest => quest.Quest.Id == savedQuest.Id).Quest.QuestConditionType)
                    {
                        case QuestConditionType.Activate:
                        {
                            // TODO:
                            _questActivateCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Get: 
                        {
                            // TODO:
                            _questGetCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Assign: 
                        {
                            // TODO:
                            _questAssignConditon.Activate();
                            break;
                        }

                        case QuestConditionType.Build:
                        {
                            // TODO:
                            _questBuildCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Collect:
                        {
                            // TODO:
                            _questCollectCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Kill:
                        {
                            // TODO:
                            _questKillCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Upgrade:
                        {
                            // TODO:
                            _questUpgradeCondition.Activate();
                            break;
                        }

                        case QuestConditionType.Destroy:
                        {
                            // TODO:
                            _questDestroyCondition.Activate();
                            break;
                        }
                    }
                }        
            }
        }

        private void ActivateQuest(int questId/*, Action<QuestBase> action*/)
        {
           //_quests.FirstOrDefault(quest => quest.Data.Id == questId).Activate(action);
        }

        public void DeactivateQuest(int questId)
        {
          // _quests.FirstOrDefault(quest => quest.Data.Id == questId).Deactivate();
        }
/*
        public QuestBase ReplaceQuest(QuestBase questToRemove) 
        {
            QuestBase nextQuest = null;
         
            Data.Settings.Quest curQuestConfig = GetQuestById(questToRemove.Data.Id);

            Data.Settings.Quest nextQuestConfig = GetNextQuestInThread(curQuestConfig.ThreadId, curQuestConfig);
          
            if (nextQuestConfig != null)
            { 
                nextQuest = AddQuestToList(nextQuestConfig);

                if (nextQuest != null) 
                {
                    questToRemove.Deactivate();

                   
                   _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, $"Replacing quest with ID {questToRemove.Data.Id} by quest {nextQuest.Data.Id}.",
                                    LogOutputLocationType.Console);
                }
            }

            return nextQuest;
        }*/

        public void ForceCompleteQuest(/*QuestBase quest*/)
        {
           //  RemoveQuestById(quest.Data.Id); 

            //new OnQuestCompleteEvent(quest, true).Invoke();
        }

        public void Dispose() 
        {/*
            if (_quests.Count == 0) return;

            foreach(var quest in _quests) quest.Dispose();*/
        }  
        
        private void CompleteQuest(/*QuestBase questBase*/)
        {
          //  var nextQuest = ReplaceQuest(questBase);

            //new OnQuestProgressEvent(questBase.Data.Id, true).Invoke();

           // new OnQuestCompleteEvent(questBase).Invoke();
         
           // new OnQuestReplacedEvent(questBase, nextQuest).Invoke();
        }

/*
        private QuestBase AddQuestToList(Data.Settings.Quest quest)
        {
           QuestBase newQuest = null;
           /*

           if(_quests.Any(qst => qst.Data.Id == quest.Id))
                _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Warning, "Found quests with the same id " + quest.Id.ToString() + " in the flow!",
                                    LogOutputLocationType.Console);
            else
            {
                newQuest = CreateQuest(QuestServiceExtensions.GetQuestTypeByCondition(quest.QuestConditionType), quest);

                _quests.Add(newQuest);

                ActivateQuest(newQuest.Data.Id, AddQuestProgress);
            }

           return newQuest;
        }
      
        private void AddQuestProgress(QuestBase questBase)
        {
            // TODO:
            _questModel.UpdateModelData(new QuestSaveData()
            {

            });

            if(questBase.QuestState == QuestState.Complete) CompleteQuest(questBase);
        }

        private QuestBase CreateQuest(Type type, Data.Settings.Quest quest)
        {
            var innerQuest = (QuestBase) _diContainer.Instantiate(type);

            innerQuest.Configurate(quest);

            return innerQuest;
        }

        private  Data.Settings.Quest GetQuestById(int id)
        {
            return _questsSettings.FirstOrDefault(quest => quest.Quest.Id == id).Quest;
        }
*/
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