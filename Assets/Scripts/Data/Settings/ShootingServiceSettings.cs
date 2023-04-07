using System;
using UnityEngine;

namespace Data.Settings
{ 
    [Serializable]
    public class ShootingElement
    {
        public string Id;

        public string Description;

        public bool IsActive;

        public float ShootRate;

        public float ShootSpeed;

        public Sprite Icon;

         public Sprite DefaultIcon;
    }

    [Serializable]
    public class ShootingServiceSettings : IRegistryData
    {  
        public string Id;

        public ShootingElement ShootingElement;

        string IRegistryData.Id => Id;
    }
}