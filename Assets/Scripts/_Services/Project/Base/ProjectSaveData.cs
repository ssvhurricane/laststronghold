using Data;
using System.Collections.Generic;
using Services.Localization;

namespace Services.Project
{
    public class GameSettingsSaveData
    {
        public float LookSensitivity;
        public bool Audio;
        public bool FrameRateCount;
        public bool Shadows;
        public Language ChoosenLanguage { get; set; }
    }

    public class ProjectSaveData : ISaveData
    {
         public int Id { get; set; }

         public int CurrentQuestFlowId {get; set; }

         public Dictionary<int, bool> QuestFlows { get; set; }
       
         public GameSettingsSaveData GameSettingsSaveData { get; set; }

         public ProjectSaveData(){}
    }
}