using System;
using Data.Settings;
using UnityEngine;

 namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "SaveDataServiceSettingsRegistry", menuName = "Registry/SaveData Service Settings Registry")]
    public class SaveDataServiceSettingsRegistry : RegistryBase<SaveDataServiceSettings>
    {

    }
}