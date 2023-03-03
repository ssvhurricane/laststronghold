using System.Collections.Generic;
using System.Linq;

namespace Services.Quest
{
    public class PlayerQuestContainer : IQuestContainer
    { 
        public List<QuestSaveData> QuestSaves { get ; set; } = new List<QuestSaveData>();

        public void SaveQuest(QuestBase quest) 
        {
            QuestSaves.Add(quest.GetSaveData());
        }

        public void RemoveQuest(QuestBase quest) 
        {
            var questBaseData = QuestSaves.FirstOrDefault(data => data.Id == quest.Data.Id);

            if (questBaseData != null)  QuestSaves.Remove(questBaseData);
        }

        public void UpdateQuest(QuestBase quest)
        {
            var questBaseData = QuestSaves.FirstOrDefault(saveQuest => saveQuest.Id == quest.Data.Id);

            if (questBaseData != null)
            {
                QuestSaves.Remove(questBaseData);

                QuestSaves.Add(quest.GetSaveData());
            }
            else 
                QuestSaves.Add(quest.GetSaveData());
        }

        public void SaveQuests(List<QuestBase> quests)
        {
            QuestSaves = new List<QuestSaveData>();

            foreach (var quest in quests)
            {
                QuestSaves.Add(quest.GetSaveData());
            }
        }

        public void LoadQuests(List<QuestBase> quests)
        {
            if (QuestSaves.Count > 0)
            {
                for (var i = 0; i < quests.Count; i++)
                {
                    var save = QuestSaves.Count > i ? QuestSaves[i] : null;
                    if (save == null)
                    {
                        save = quests[i].GetSaveData();
                        
                    }
                    quests[i].Load(save);
                }
            }
        }

        public void Clear()
        {
            QuestSaves.Clear();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
