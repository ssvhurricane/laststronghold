using System;
using UnityEngine;

namespace Data.Settings
{ 
    [Serializable]
    public class InteractElement
    {
        public string Id;

        public string Description;

        public bool IsActive;

        public float InteractionTime; // get from cur area.

        public Sprite Icon;
    }

    [Serializable]
    public class InteractionServiceSettings : IRegistryData
    {  
        public string Id;

        public InteractElement InteractElement;

        string IRegistryData.Id => Id;
    }
}