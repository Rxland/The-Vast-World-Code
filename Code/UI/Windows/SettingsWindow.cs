using System;
using _GAME.Code.Features;
using _GAME.Code.Tools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Slider _gameVolumeSlider;
        
        [Header("Graphic")]
        [SerializeField] private Toggle _lowGraphicToggle;
        [SerializeField] private Toggle _mediumGraphicToggle;
        [SerializeField] private Toggle _ultraGraphicToggle;
        
        [Header("Graphic")]
        [SerializeField] private Toggle _ruLanguageToggle;
        [SerializeField] private Toggle _enLanguageToggle;
        [SerializeField] private Toggle _jaLanguageToggle;
        
        [Inject] private SettingsFeature _settingsFeature;
        
        public override void OpenWindow()
        {
            base.OpenWindow();
            
            _gameVolumeSlider.onValueChanged.AddListener(OnGameVolumeSliderChanged);
            
            _gameVolumeSlider.value = _settingsFeature.SettingsSaveData.MainVolume;

            InitGraphicSettings();
            InitLanguage();
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
            CursorFeature.SetCursorLockedMode(false);
        }
        
        private void OnGameVolumeSliderChanged(float value)
        {
            _settingsFeature.ChangeMainVolume(value);
        }

        
        #region Graphic

        private void InitGraphicSettings()
        {
            switch (_settingsFeature.SettingsSaveData.GraphicsType)
            {
                case GraphicsType.Low:
                    LowSettings(true);
                    break;
                case GraphicsType.Medium:
                    MediumSettings(true);
                    break;
                case GraphicsType.Ultra:
                    UltraSettings(true);
                    break;
            }
        }

        public void LowSettings(bool toggleBool)
        {
            _lowGraphicToggle.isOn = toggleBool;
            
            if (!toggleBool) return;

            _settingsFeature.ChangeGraphics(GraphicsType.Low);
        }
        
        public void MediumSettings(bool toggleBool)
        {
            _mediumGraphicToggle.isOn = toggleBool;
            
            if (!toggleBool) return;
            
            _settingsFeature.ChangeGraphics(GraphicsType.Medium);
        }

        public void UltraSettings(bool toggleBool)
        {
            _ultraGraphicToggle.isOn = toggleBool;
            
            if (!toggleBool) return;
            
            _settingsFeature.ChangeGraphics(GraphicsType.Ultra);
        }
        
        #endregion

        #region Language

        private void InitLanguage()
        {
            switch (_settingsFeature.SettingsSaveData.LanguageType)
            {
                case Language.English:
                    SetEnLanguage(true);
                    break;
                case Language.Russian:
                    SetRuLanguage(true);
                    break;
                case Language.Japanese:
                    SetJapaneseLanguage(true);
                    break;
            }
        }
        
        public void SetRuLanguage(bool toggleBool)
        {
            _ruLanguageToggle.isOn = toggleBool;
            
            if (!toggleBool) return;
            
            _settingsFeature.ChangeLanguage(Language.Russian);
        }

        public void SetEnLanguage(bool toggleBool)
        {
            _enLanguageToggle.isOn = toggleBool;
            
            if (!toggleBool) return;
            
            _settingsFeature.ChangeLanguage(Language.English);
        }
        
        public void SetJapaneseLanguage(bool toggleBool)
        {
            _jaLanguageToggle.isOn = toggleBool;
            
            if (!toggleBool) return;
            
            _settingsFeature.ChangeLanguage(Language.Japanese);
        }
        
        #endregion
        
    }
}