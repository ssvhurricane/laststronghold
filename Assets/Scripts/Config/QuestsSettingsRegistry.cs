using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "QuestsSettingsRegistry", menuName = "Registry/Quests Settings Registry")]
    public class QuestsSettingsRegistry : RegistryListBase<QuestsSettings>
    {

    }
}
