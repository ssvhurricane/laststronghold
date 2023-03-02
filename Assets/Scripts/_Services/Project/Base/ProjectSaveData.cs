using Data;
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

         public int QuestFlowId {get; set; }

         public GameSettingsSaveData GameSettingsSaveData { get; set; }

         public ProjectSaveData(){}
    }
}