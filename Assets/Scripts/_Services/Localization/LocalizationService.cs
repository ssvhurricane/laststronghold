using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Data.Settings;
using Model;
using UnityEngine.UI;
using Zenject;

namespace Services.Localization
{
    public class LocalizationService
    {
        private readonly SignalBus _signalBus;

        private readonly LocalizationServiceSettings[] _localizationServiceSettings;

        private readonly ProjectModel _projectModel;

        private string _currentSystemLocalization  = CultureInfo.CurrentCulture.Name;

        public readonly IReadOnlyList<LanguageInfo> IngameLanguages = new List<LanguageInfo>()
        {
            new LanguageInfo(Language.EN, "English").AddLanguageCode("en"),
            new LanguageInfo(Language.DE, "Deutsche").AddLanguageCode("de"),
            new LanguageInfo(Language.RU, "Русский").AddLanguageCode("ru"),
            new LanguageInfo(Language.ES, "Español").AddLanguageCode("es"),
            new LanguageInfo(Language.FR, "Français").AddLanguageCode("fr"),
            new LanguageInfo(Language.IT, "Italiano").AddLanguageCode("it"),
            new LanguageInfo(Language.BR, "Portuguese (Brazil)").AddLanguageCodeRegion("pt-br"),
            new LanguageInfo(Language.PT, "Portuguese").AddLanguageCodeRegion("pt-pt"),
            new LanguageInfo(Language.DA, "Dansk").AddLanguageCodeRegion("dk"),
            new LanguageInfo(Language.NO, "Norsk").AddLanguageCodeRegion("no"),
            new LanguageInfo(Language.NL, "Nederlands").AddLanguageCodeRegion("nl"),
            new LanguageInfo(Language.SE, "Svenska").AddLanguageCodeRegion("sv"),
            new LanguageInfo(Language.JA, "日本語").AddLanguageCodeRegion("ja"),
            new LanguageInfo(Language.KO, "한국어").AddLanguageCodeRegion("ko"),
            new LanguageInfo(Language.TW, "台湾").AddLanguageCodeRegion("tw"),
        };

        private Dictionary<string, Text> _translatedTexts;

        public LocalizationService(SignalBus signalBus, 
                                LocalizationServiceSettings[] localizationServiceSettings,
                                ProjectModel projectModel)
        {
            _signalBus = signalBus; 

            _localizationServiceSettings = localizationServiceSettings;

            _projectModel = projectModel;

            _translatedTexts =  new Dictionary<string, Text>();

            CurrentLanguage = _projectModel.GetProjectSaveDataAsReactive().Value.GameSettingsSaveData.ChoosenLanguage;
            
            if (CurrentLanguage == Language.Undefined)
            {
                ChangeLanguage(GetSystemLanguage());
            }
        }

        public bool HaveKey(string key) => _localizationServiceSettings?.Any(item =>item.localizationItem.Key == key) ?? false;

        public Language CurrentLanguage { get; private set; }

        public LanguageInfo GetLanguageInfo(Language language)
        {
            return IngameLanguages.FirstOrDefault(info => info.Language == language);
        }

        public string Translate(string key, Text text = null)
        {
            string translateText = string.Empty;

            //if(!_translatedTexts.ContainsKey(key)) _translatedTexts.Add(key, text);
            
            switch(CurrentLanguage)
            {
                case Language.RU:
                {
                    translateText = _localizationServiceSettings?.FirstOrDefault(item => item.localizationItem.Key == key).localizationItem.RU;
                   
                    break;
                }
                case Language.EN:
                { 
                    translateText = _localizationServiceSettings?.FirstOrDefault(item => item.localizationItem.Key == key).localizationItem.EN;

                    break;
                }
            }

            return translateText;
        }

        public string Translate(string key, params string[] inputs)
        {
            var formatString = Translate(key);
            if (!string.IsNullOrEmpty(formatString) && inputs.Length > 0)
            {
                var _inputs = inputs
                    .Select(item => Translate(item))
                    .ToArray();

                formatString = string.Format(formatString, _inputs);
            }
            return formatString;
        }

        public void ChangeLanguage(Language newLanguage) 
        {
           CurrentLanguage = newLanguage;

           // TODO:_projectModel.UpdateGamesettings
        }

        public Language GetSystemLanguage()
        {
            return IngameLanguages.FirstOrDefault(info => info.IsCurrentLanguage(_currentSystemLocalization))
                       ?.Language ??
                   Language.EN;
        }

        public void UpdateLocalization()
        {
            // TODO: need update runtime, _translatedTexts
        }
    }
}
