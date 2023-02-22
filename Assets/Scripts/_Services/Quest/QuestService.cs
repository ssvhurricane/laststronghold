using System;
using System.Collections.Generic;
using Data.Settings;
using Services.Scene;
using Zenject;
using Model;
using System.Linq;
using Services.Log;

namespace Services.Quest
{
    public class QuestService
    {
        private List<QuestBase> _quests;
        private Flow _flow;
        private readonly SignalBus _signalBus;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private readonly ISceneService _sceneService;
        private readonly LogService _logService; 
        private readonly QuestModel _questModel;
        private readonly DiContainer _diContainer; //TODO:ref

        private Dictionary<int, List<Data.Settings.Quest>> _questsThreads;
        public QuestService(SignalBus signalBus, 
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            ISceneService sceneService, 
                            LogService logService,
                            QuestModel questModel,
                            DiContainer diContainer)
        {
            _signalBus = signalBus;

            _questServiceSettings = questServiceSettings;

            _questsSettings = questsSettings;

            _sceneService = sceneService;

            _logService = logService;

            _questModel = questModel;

            _diContainer = diContainer;

            _quests = new List<QuestBase>();

            _questsThreads = new Dictionary<int, List<Data.Settings.Quest>>();

            LoadRegistryData();
        }

        public void InitializeFlow(Flow flow)
        {
            _flow = flow;
            _flow.Parse();

            var savedQuests = _questModel.GetPlayerQuestContainer().QuestSaves;

            // Load saved quests.
            if(savedQuests != null && savedQuests.Count > 0)
                foreach(var savedQuest in savedQuests) AddQuestToList(GetQuestById(savedQuest.Id)).Load(savedQuest);
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
            }
        }

        public void ClearActiveQuests()
        {
            if(_quests != null) _quests.Clear();

            if (_questModel.GetPlayerQuestContainer().QuestSaves.Count() > 0)
                _questModel.GetPlayerQuestContainer().QuestSaves.Clear();

            // new OnAllQuestsInFlowCompleteEvent().Invoke();
        }

        public void AddQuestById(int questId)
        {
            if (_quests.Any(quest => quest.Data.Id != questId)) 
            {
                var newQuest = AddQuestToList(GetQuestById(questId));

                if (newQuest != null) 
                { 
                    if (_questModel.GetPlayerQuestContainer().QuestSaves.Any(data => data.Id != questId))
                             _questModel.GetPlayerQuestContainer().SaveQuest(newQuest);
                }
            }
        }

        public void RemoveQuestById(int questId)
        {
            DeactivateQuest(questId);

            _quests.RemoveAll(x => x.Data.Id == questId);
           
            var savedData = _questModel.GetPlayerQuestContainer().QuestSaves.FirstOrDefault(questData => questData.Id == questId);

            if (savedData != null)
                 _questModel.GetPlayerQuestContainer().QuestSaves.Remove(savedData);
        }

        public void ActivateQuest(int questId, Action<QuestBase> action)
        {
           _quests.FirstOrDefault(quest => quest.Data.Id == questId).Activate(action);
        }

        public void DeactivateQuest(int questId)
        {
           _quests.FirstOrDefault(quest => quest.Data.Id == questId).Deactivate();
        }

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

                    RemoveQuestById(questToRemove.Data.Id);

                    _questModel.GetPlayerQuestContainer().SaveQuest(nextQuest);

                   _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, $"Replacing quest with ID {questToRemove.Data.Id} by quest {nextQuest.Data.Id}.",
                                    LogOutputLocationType.Console);
                }
            }

            return nextQuest;
        }

        public void ForceCompleteQuest(QuestBase quest)
        {
             RemoveQuestById(quest.Data.Id); 

            //new OnQuestCompleteEvent(quest, true).Invoke();
        }

        public void Dispose() 
        {
            if (_quests.Count == 0) return;

            foreach(var quest in _quests) quest.Dispose();
        }  
        
        private void CompleteQuest(QuestBase questBase)
        {
            var nextQuest = ReplaceQuest(questBase);

            //new OnQuestProgressEvent(questBase.Data.Id, true).Invoke();

           // new OnQuestCompleteEvent(questBase).Invoke();
         
           // new OnQuestReplacedEvent(questBase, nextQuest).Invoke();
        }

        private QuestBase AddQuestToList(Data.Settings.Quest quest)
        {
           QuestBase newQuest = null;

           if(_quests.Any(qst => qst.Data.Id == quest.Id))
                _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Error! Found quests with the same id " + quest.Id.ToString() + " in the flow!",
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
            _questModel.GetPlayerQuestContainer().UpdateQuest(questBase);

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
    }
}