using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "CheatServiceSettingsRegistry", menuName = "Registry/Cheat Service Settings Registry")]
    public class CheatServiceSettingsRegistry : RegistryBase<CheatServiceSettings>
    {

    }
}