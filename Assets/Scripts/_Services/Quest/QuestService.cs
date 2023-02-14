using System;
using Zenject;

namespace Services.Quest
{
    public class QuestService
    {
        private readonly SignalBus _signalBus;

        public QuestService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void InitializeFlow(FlowQuestsConfig flowQuestsConfig)
        {
            // TODO:
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