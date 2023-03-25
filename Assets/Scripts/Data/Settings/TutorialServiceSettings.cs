using System;
using Services.Tutorial;
using UnityEngine;

namespace Data.Settings
{  
    [Serializable]
    public class Tutorial 
    {
        public int Id;

        public string Description;

        public TutorialMode TutorialMode;
      
        public Sprite[] Sprites;

        public TextAsset[] StepsDescriptions;
    }

    [Serializable]
    public class TutorialServiceSettings : IRegistryData
    {
        public string Id;
      
        public Tutorial Tutorial;
        string IRegistryData.Id => Id;
    }
}