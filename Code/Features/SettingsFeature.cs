using _GAME.Code.Factories;
using _GAME.Code.Save_Data;
using _GAME.Code.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class SettingsFeature
    {
        [ReadOnly] public SettingsSaveData SettingsSaveData; 
        
        [Inject] private SaveFeature _saveFeature;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private CameraFeature _cameraFeature;
        
        
        public void InitGameSettings()
        {
            SettingsSaveData = _saveFeature.SaveData.SettingsSaveData;

            InitGameVolume();
            UpdateCurrentLanguage();
            UpdateGraphics();
        }

        public void InitGameVolume()
        {
            AudioListener.volume = SettingsSaveData.MainVolume;
        }
        
        public void ChangeMainVolume(float volume)
        {
            SettingsSaveData.MainVolume = volume;
            AudioListener.volume = volume;
            
            _saveFeature.SaveGame();
        }

        public void ChangeGraphics(GraphicsType graphicsType)
        {
            SettingsSaveData.GraphicsType = graphicsType;

            UpdateGraphics();
            
            _saveFeature.SaveGame();
        }
        
        public void ChangeLanguage(Language languageType)
        {
            SettingsSaveData.LanguageType = languageType;

            UpdateCurrentLanguage();
            
            _windowFactory.InventoryWindow?.ReOpenWindow();
            
            _saveFeature.SaveGame();
        }

        private void UpdateCurrentLanguage()
        {
            switch (SettingsSaveData.LanguageType)
            {
                case Language.Russian:
                    I2.Loc.LocalizationManager.CurrentLanguage = "Russian";
                    break;
                case Language.English:
                    I2.Loc.LocalizationManager.CurrentLanguage = "English";
                    break;
                case Language.Japanese:
                    I2.Loc.LocalizationManager.CurrentLanguage = "Japanese";
                    break;
            }
        }
        
        private void UpdateGraphics()
        {
            int level = (int)SettingsSaveData.GraphicsType;
            
            switch (level)
            {
                case 0:
                    QualitySettings.SetQualityLevel(0, false);
                    _cameraFeature.CameraFarClipPlane = 60f;
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(1, false);
                    _cameraFeature.CameraFarClipPlane = 500f;
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2, false);
                    _cameraFeature.CameraFarClipPlane = 1000f;
                    break;
            }
        }
    }
}