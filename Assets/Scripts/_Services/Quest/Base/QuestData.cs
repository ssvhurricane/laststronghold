using System.Collections.Generic;

namespace Services.Quest
{
    public class QuestData 
    {
        public int Id { get; set; }

        public QuestConditionType QuestConditionType { get; set; }

        public List<string> DroptItems { get; set; } // TODO: Drop Items

        public string Description { get; set; }

        public int CurProgress { get; set; }

        public int NeedProgress { get; set; }
    }
}