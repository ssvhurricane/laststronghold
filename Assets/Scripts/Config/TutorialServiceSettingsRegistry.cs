using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "TutorialServiceSettingsRegistry", menuName = "Registry/Tutorial Service Settings Registry")]
    public class TutorialServiceSettingsRegistry : RegistryListBase<TutorialServiceSettings>
    {

    }
}
