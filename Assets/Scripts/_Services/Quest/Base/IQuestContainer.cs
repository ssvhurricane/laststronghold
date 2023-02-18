using System.Collections.Generic;

namespace Services.Quest
{
    public interface IQuestContainer 
    {  
          public List<QuestSaveDataBase> QuestSaves{ get; set; }
          public void Initialize();
          public void SaveQuest(QuestBase quest);
          public void RemoveQuest(QuestBase quest);
          public void UpdateQuest(QuestBase quest);
          public void SaveQuests(List<QuestBase> quests);
          public void LoadQuests(List<QuestBase> quests);
          
          public void Clear();
    }
}