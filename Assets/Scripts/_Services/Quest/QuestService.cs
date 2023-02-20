using System;
using System.Collections.Generic;
using Data.Settings;
using Services.Scene;
using Zenject;
using Presenters;
using Model;
using System.Linq;
using Services.Log;

namespace Services.Quest
{
    public class QuestService
    {
        public List<QuestBase> _quests { get; private set; }
        private Flow _flow;
        private readonly SignalBus _signalBus;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly QuestsSettings[] _questsSettings;
        private readonly ISceneService _sceneService;
        private readonly LogService _logService; 
        private readonly PlayerPresenter _playerPresenter;
        private readonly DiContainer _diContainer; //TODO:ref
        public QuestService(SignalBus signalBus, 
                            QuestServiceSettings questServiceSettings,
                            QuestsSettings[] questsSettings,
                            ISceneService sceneService, 
                            LogService logService,
                            PlayerPresenter playerPresenter,
                            DiContainer diContainer)
        {
            _signalBus = signalBus;

            _questServiceSettings = questServiceSettings;

            _questsSettings = questsSettings;

            _sceneService = sceneService;

            _logService = logService;

            _playerPresenter = playerPresenter;

            _diContainer = diContainer;

            _quests = new List<QuestBase>();
        }

        public void InitializeFlow(Flow flow)
        {
            _flow = flow;

            var playerModel = (QuestModel)(_playerPresenter.GetModel());

            var savedQuests = playerModel.GetPlayerQuestContainer().QuestSaves;

            // Load saved quests.
            if(savedQuests != null && savedQuests.Count > 0)
            {
                foreach(var savedQuest in savedQuests) AddQuestToList(GetQuestById(savedQuest.QuestId)).Load(savedQuest);
            }
            else
            {
                // Create new quests.
                foreach(var thread in flow.ParseThreads)
                {
                    var quest = GetThreadByKey(thread).Value?.FirstOrDefault();

                    if (quest != null) 
                    { 
                        if(!string.IsNullOrEmpty(quest.Tag) || !string.IsNullOrEmpty(quest.Value))
                               playerModel.GetPlayerQuestContainer().SaveQuest(AddQuestToList(quest));
                    }
                }
            }

           // if(flow.ParseThreads != null && flow.ParseThreads.Count != 0) new OnQuestInitializeFlowEvent(_quests).Invoke();
        }

        public void ClearActiveQuests()
        {
            if(_quests != null) _quests.Clear();

            var playerModel = (QuestModel)(_playerPresenter.GetModel());

            if (playerModel.GetPlayerQuestContainer().QuestSaves.Count() > 0)
                playerModel.GetPlayerQuestContainer().QuestSaves.Clear();

            // new OnAllQuestsInFlowCompleteEvent().Invoke();
        }

        public void AddQuestById(int questId)
        {
            if (_quests.Any(quest => quest.Data.Id != questId)) 
            {
                var newQuest = AddQuestToList(GetQuestById(questId));

                var playerModel = (QuestModel)(_playerPresenter.GetModel());

                if (newQuest != null) 
                { 
                    if ( playerModel.GetPlayerQuestContainer().QuestSaves.Any(data => data.QuestId != questId))
                             playerModel.GetPlayerQuestContainer().SaveQuest(newQuest);
                }
            }
        }

        public void RemoveQuestById(int questId)
        {
            DeactivateQuest(questId);

            _quests.RemoveAll(x => x.Data.Id == questId);

            var playerModel = (QuestModel)(_playerPresenter.GetModel());

            var savedData = playerModel.GetPlayerQuestContainer().QuestSaves.FirstOrDefault(questData => questData.QuestId == questId);

            if (savedData != null)
                 playerModel.GetPlayerQuestContainer().QuestSaves.Remove(savedData);
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

            var playerModel = (QuestModel)(_playerPresenter.GetModel());

            Data.Settings.Quest curQuestConfig = GetQuestById(questToRemove.Data.Id);

            Data.Settings.Quest nextQuestConfig = GetNextQuestInThread(curQuestConfig.ThreadId, curQuestConfig);
          
            if (nextQuestConfig != null)
            { 
                nextQuest = AddQuestToList(nextQuestConfig);

                if (nextQuest != null) 
                {
                    questToRemove.Deactivate();

                    RemoveQuestById(questToRemove.Data.Id);

                    playerModel.GetPlayerQuestContainer().SaveQuest(nextQuest);

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
            var playerModel = (QuestModel)(_playerPresenter.GetModel());

            playerModel.GetPlayerQuestContainer().UpdateQuest(questBase);

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

        private Data.Settings.Quest[] GetAllQuests()
        {
            // TODO:
            return null;
        }

         private Data.Settings.Quest GetNextQuestInThread(int threadId, Data.Settings.Quest currentQuest)
         {
            // TODO:
             return null;
         }

         private KeyValuePair<int, List<Data.Settings.Quest>> GetThreadByKey(int key)
         {
            // TODO:
            return new KeyValuePair<int, List<Data.Settings.Quest>>();
         }
    }
}