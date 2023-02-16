using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "QuestServiceSettingsRegistry", menuName = "Registry/Quest Service Settings Registry")]
    public class QuestServiceSettingsRegistry : RegistryBase<QuestServiceSettings>
    {

    }
}
