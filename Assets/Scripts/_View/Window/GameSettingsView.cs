using System.Collections.Generic;
using Services.Localization;
using Services.Project;
using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/GameSettingsView", WindowType.PopUpWindow)]
    public class GameSettingsView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected Slider LookSensitivitySlider;

        [SerializeField] protected Toggle AudioEnabledToggle;

        [SerializeField] protected Toggle FramerateCounterEnabledToggle;

        [SerializeField] protected Toggle ShadowsEnabledToggle;

        [SerializeField] protected Dropdown LanguageSelectFropDown;

        [SerializeField] public Button _backButton;

        [SerializeField] public Button _applySettingsButton;

        private SignalBus _signalBus;
        private  LocalizationService _localizationService;

        [Inject]
        public void Constrcut(SignalBus signalBus,
        LocalizationService localizationService)
        {
            _signalBus = signalBus;

            _localizationService = localizationService;

            WindowType = Type;

            LanguageSelectFropDown.options = new List<Dropdown.OptionData>();

            foreach(var option in  _localizationService.IngameLanguages)
            {
                 LanguageSelectFropDown.options.Add(new Dropdown.OptionData(option.LanguageName));
            }

            _signalBus.Fire(new WindowServiceSignals.Register(this));

            _applySettingsButton.onClick.AddListener(OnApplyButton);

            LookSensitivitySlider.onValueChanged.AddListener(OnSliderChanged);

        }

        private void OnApplyButton()
        {
            var data = new GameSettingsViewArgs();
            data.ProjectData = new ProjectSaveData();
            data.ProjectData.Id = -1;
            data.ProjectData.CurrentQuestFlowId = -1;
            data.ProjectData.GameSettingsSaveData = new GameSettingsSaveData();
            data.ProjectData.GameSettingsSaveData.LookSensitivity = LookSensitivitySlider.value;
            data.ProjectData.GameSettingsSaveData.Audio = AudioEnabledToggle.isOn;
            data.ProjectData.GameSettingsSaveData.FrameRateCount = FramerateCounterEnabledToggle.isOn;
            data.ProjectData.GameSettingsSaveData.Shadows = ShadowsEnabledToggle.isOn;
            data.ProjectData.GameSettingsSaveData.ChoosenLanguage = (Language) LanguageSelectFropDown.value;
           
            _signalBus.Fire(new GameSettingsViewSignals.Apply(this.GetType().Name, data));
        }

        private void OnSliderChanged(float value)
        {
            // TODO:
        }


        public void UpdateView(GameSettingsViewArgs gameSettingsViewArgs)
        {
            // The view should not change the model directly!
            LookSensitivitySlider.value = gameSettingsViewArgs.ProjectData.GameSettingsSaveData.LookSensitivity;

            AudioEnabledToggle.isOn = gameSettingsViewArgs.ProjectData.GameSettingsSaveData.Audio;

            FramerateCounterEnabledToggle.isOn = gameSettingsViewArgs.ProjectData.GameSettingsSaveData.FrameRateCount;

            ShadowsEnabledToggle.isOn = gameSettingsViewArgs.ProjectData.GameSettingsSaveData.Shadows;

            LanguageSelectFropDown.value = (int) gameSettingsViewArgs.ProjectData.GameSettingsSaveData.ChoosenLanguage;
        }
    }
    public class GameSettingsViewArgs : IWindowArgs
    {
        public ProjectSaveData ProjectData { get; set; }

        public GameSettingsViewArgs(){}
        public GameSettingsViewArgs(ProjectSaveData projectData)
        {
            ProjectData = projectData;
        }
    }
}