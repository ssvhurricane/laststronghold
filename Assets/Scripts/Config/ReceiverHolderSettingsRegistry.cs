using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ReceiverHolderSettingsRegistry", menuName = "Registry/Receiver Holder Settings Registry")]
    public class ReceiverHolderSettingsRegistry : RegistryListBase<ReceiverHolderSettings>
    {
      
    }
}