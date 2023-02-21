using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Services.Quest
{
    public interface IQuestContainer 
    {  
          public ObservableCollection<QuestSaveData> QuestSaves{ get; set; }
          public void Initialize();
          public void SaveQuest(QuestBase quest);
          public void RemoveQuest(QuestBase quest);
          public void UpdateQuest(QuestBase quest);
          public void SaveQuests(List<QuestBase> quests);
          public void LoadQuests(List<QuestBase> quests);
          
          public void Clear();
    }
}