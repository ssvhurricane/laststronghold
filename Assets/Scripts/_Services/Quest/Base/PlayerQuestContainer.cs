using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Services.Quest
{
    public class PlayerQuestContainer : IQuestContainer
    { 
        [SerializeReference] private ObservableCollection<QuestSaveData> _questSaves = new ObservableCollection<QuestSaveData>();
        public ObservableCollection<QuestSaveData> QuestSaves { get; set; }
        public void Initialize()
        {
            QuestSaves = _questSaves;
        }
        public void SaveQuest(QuestBase quest) 
        {
            _questSaves.Add(quest.GetSaveData());
        }

        public void RemoveQuest(QuestBase quest) 
        {
            var questBaseData =_questSaves.FirstOrDefault(data => data.Id == quest.Data.Id);

            if (questBaseData != null)  _questSaves.Remove(questBaseData);
        }

        public void UpdateQuest(QuestBase quest)
        {
            var questBaseData = _questSaves.FirstOrDefault(saveQuest => saveQuest.Id == quest.Data.Id);

            if (questBaseData != null)
            {
                _questSaves.Remove(questBaseData);

                _questSaves.Add(quest.GetSaveData());
            }
            else 
                _questSaves.Add(quest.GetSaveData());
        }

        public void SaveQuests(List<QuestBase> quests)
        {
            _questSaves = new ObservableCollection<QuestSaveData>();

            foreach (var quest in quests)
            {
                _questSaves.Add(quest.GetSaveData());
            }
        }

        public void LoadQuests(List<QuestBase> quests)
        {
            if (_questSaves.Count > 0)
            {
                for (var i = 0; i < quests.Count; i++)
                {
                    var save = _questSaves.Count > i ? _questSaves[i] : null;
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
            _questSaves.Clear();
        }
    }
}
