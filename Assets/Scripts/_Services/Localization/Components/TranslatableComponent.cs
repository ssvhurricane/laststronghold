using Services.Log;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Localization
{
    public class TranslatableComponent : MonoBehaviour
    {
        private SignalBus _signalBus;
        private LocalizationService _localizationService;
        private LogService _logService;

        [SerializeField] private bool _isUpdateOnStart = true;
        [SerializeField] private string _key;
        private Text _text;

        [Inject]
        public void Construct(SignalBus signalBus, LocalizationService localizationService, LogService logService) 
        {
            _signalBus = signalBus;
            _localizationService = localizationService;
            _logService = logService;

            if (_isUpdateOnStart) UpdateLabel();
        }
        public void UpdateLabel()
        {
            _text = GetComponent<Text>();

            if(_text != null)
                 _text.text =_localizationService.Translate(_key);
            else 
            _logService.ShowLog(GetType().Name, 
                    Log.LogType.Error, 
                    ($"Object have no text for translate, need component Text!"),
                    LogOutputLocationType.Console);

        }
    }
}
