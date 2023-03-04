using Config;
using System;
using UnityEngine;
using Services.Project;
using Services.Localization;

namespace Data.Settings
{
    [Serializable]
    public class GameSettingsData
    {
        public float LookSensitivity;
        public bool Audio;
        public bool FrameRateCount;
        public bool Shadows;
        public Language ChoosenLanguage;
    }

    [Serializable]
    public class ProjectServiceSettings : IRegistryData
    {
        [SerializeField] public string ProjectName;
        [SerializeField] public GameVersion GameDataVersion;

        public string Id
        {
            get { return ProjectName; }
        }

        [Serializable]
        public class StartupSettings 
        {

        }

        [SerializeField]
        public ProjectMode ProjectMode;

        [SerializeField]
        public ProjectType ProjectType;

        [Serializable]
        public class GameVersion 
        {
            public long Major;
            public long Minor;

            public override string ToString()
            {
                return $"{Major}.{Minor}";
            }

            public bool AreEqual(GameVersion other)
            {
                return Major == other.Major && Minor == other.Minor;
            }
        }
        [SerializeField]
        public GameSettingsData GameSettingsData;

        [SerializeField]
        public int QuestStartFlowId; 
    }
}