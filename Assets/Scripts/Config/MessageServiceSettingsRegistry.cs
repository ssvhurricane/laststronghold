using System;
using Data.Settings;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "MessageServiceSettingsRegistry ", menuName = "Registry/Message Service Settings Registry")]
    public class MessageServiceSettingsRegistry : RegistryListBase<MessageServiceSettings>
    {

    }
}
