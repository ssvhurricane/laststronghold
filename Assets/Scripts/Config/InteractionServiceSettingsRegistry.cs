using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "InteractionServiceSettingsRegistry", menuName = "Registry/Interaction Service Settings Registry")]
    public class InteractionServiceSettingsRegistry : RegistryListBase<InteractionServiceSettings>
    {

    }
}
