using Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class CameraServiceSettings : IRegistryData
    {
        public string Id;

        public bool IsActive;

        public string Name;

        public Services.Camera.CameraType CameraType;

        public Vector3 Position;

        public Vector3 Rotation;

        public float CameraFollowSmoothSpeed;

        public Vector3 CameraFollowOffset;

        public List<string> Abilities;

        public List<string> Items;

        string IRegistryData.Id => Id;
    }
}