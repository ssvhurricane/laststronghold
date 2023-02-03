using Config;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class Move 
    {
        public float Speed;
        public float BlendSpeed;
        public float AirResistance;
    }

    [Serializable]
    public class Run 
    {
        public float Speed;
    }

    [Serializable]
    public class Rotate
    {
        public float Speed;
        public float UpperLimit;
        public float BottomLimit;
        public float LeftLimit;
        public float RightLimit;
        public float Sensitivity;
    }

    [Serializable]
    public class Jump
    {
        public float Speed;
        public ForceMode ForceMode;
        public LayerMask GroundLayers;
        public float Dis2Ground;
    }

    [Serializable]
    public class Crouch 
    {
        public float Speed;
    }
    [Serializable]
    public class Orbit
    {
        public float Radius;
        public GameObject Anchor;
    }

    [Serializable]
    public class MovementServiceSettings : IRegistryData
    {
        public string Id;

        public Move Move;
        public Run Run;
        public Jump Jump;
        public Rotate Rotate;
        public Crouch Crouch;
        public Orbit OrbitAnchor;

        string IRegistryData.Id => Id;
    }
}