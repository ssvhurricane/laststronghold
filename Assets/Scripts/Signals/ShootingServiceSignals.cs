using Services.Essence;
using UnityEngine;

namespace Signals
{
    public class ShootingServiceSignals 
    {
        public class Hit
        {
            public BaseEssence BaseEssence { get; }
            public Collision Collision { get; }

            public Hit(BaseEssence baseEssence, Collision collision)
            {
                BaseEssence = baseEssence;

                Collision = collision;
            }
        }
    }
}