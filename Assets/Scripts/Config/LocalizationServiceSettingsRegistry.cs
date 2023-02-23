using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "LocalizationServiceSettingsRegistry", menuName = "Registry/Localization Service Settings Registry")]
    public class LocalizationServiceSettingsRegistry : RegistryListBase<LocalizationServiceSettings>
    {

    }
}
