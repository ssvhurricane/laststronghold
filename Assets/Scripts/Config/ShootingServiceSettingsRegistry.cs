using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ShootingServiceSettingsRegistry", menuName = "Registry/Shooting Service Settings Registry")]
    public class ShootingServiceSettingsRegistry : RegistryListBase<ShootingServiceSettings>
    {

    }
}
