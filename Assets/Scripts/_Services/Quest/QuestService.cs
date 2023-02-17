using System;
using System.Collections.Generic;
using Data.Settings;
using Services.Scene;
using Zenject;

namespace Services.Quest
{
    public class QuestService
    {
        private List<QuestBase> _quests;
        private Flow _flow;
        private readonly SignalBus _signalBus;
        private readonly QuestServiceSettings _questServiceSettings;
        private readonly ISceneService _sceneService;

        public QuestService(SignalBus signalBus, 
                            QuestServiceSettings questServiceSettings,
                            ISceneService sceneService)
        {
            _signalBus = signalBus;

            _questServiceSettings = questServiceSettings;

            _sceneService = sceneService;

            _quests = new List<QuestBase>();
        }

        public void InitializeFlow(Flow flow)
        {
            _flow = flow;

           // var savedQuests = _sceneService.GetLevelsModel().GetLevelData(GameLevelManager.CurLevelID)?.QuestStorage?.QuestSaves;
              
        }

        public void ClearActiveQuests()
        {
            // TODO:
        }

        public void AddQuestById(int questId)
        {
            // TODO:
        }

        public void RemoveQuestById(int questId)
        {
            // TODO:
        }

        public void ActivateQuest(int questId, Action<QuestBase> action)
        {
            // TODO:
        }

        public void DeactivateQuest(int questId)
        {
            // TODO:
        }

        public QuestBase ReplaceQuest(QuestBase questToRemove) 
        {
            // TODO:
            return null;
        }

        public void ForceCompleteQuest(QuestBase quest)
        {
            // TODO:
        }

        public void Dispose() 
        {
            // TODO:
        }  
        
        private void CompleteQuest(QuestBase quest)
        {
            // TODO:
        }

        private QuestBase AddQuestToList(/*QuestConfig questConfig*/)
        {
           // TODO:
            return null;
        }

       
        private void AddQuestProgress(QuestBase quest)
        {
            // TODO:
        }

        private QuestBase CreateQuest(Type type)
        {
            // TODO:
            return null;
        }
    }
}