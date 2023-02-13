using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "AreaServiceSettingsRegistry", menuName = "Registry/Area Service Settings Registry")]
    public class AreaServiceSettingsRegistry : RegistryListBase<AreaServiceSettings>
    {
      
    }
}