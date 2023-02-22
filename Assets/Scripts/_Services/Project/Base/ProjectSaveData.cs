using Data;

namespace Services.Project
{
    public class ProjectSaveData : ISaveData
    {
         public int Id { get; set; }

         public int QuestFlowId {get; set; }
    }
}