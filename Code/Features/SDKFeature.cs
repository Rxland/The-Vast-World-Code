using System;
using _GAME.Code.Factories;
using _GAME.Code.Tools;
using UnityEngine;
using YG;
using Zenject;
using DeviceType = _GAME.Code.Types.DeviceType;

namespace _GAME.Code.Features
{
    public class SDKFeature : MonoBehaviour
    {
        [Inject] private WindowFactory _windowFactory;
        [Inject] private SettingsFeature _settingsFeature;
        [Inject] private DeviceFeature _deviceFeature;
        [Inject] private SaveFeature _saveFeature;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += Init;
            YandexGame.RewardVideoEvent += GetRewarded;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Init;
            YandexGame.RewardVideoEvent -= GetRewarded;
        }

        private void Init()
        {
            _saveFeature.LoadGame();
            _saveReactivePropertiesFeature.Init();
            
            _settingsFeature.ChangeLanguage(ConvertYandexLanguageType());
            
            DetectDevice();
            SetGraphicsByDevice();
            
            _windowFactory.MainMenuWindow.TurnOnViewElements();
        }
        
        public void ShowFullScreen()
        {
            YandexGame.FullscreenShow();
        }
        
        public void ShowRewardVideoRevive()
        {
            YandexGame.RewVideoShow(0);
        }
        
        public void ShowRewardVideoOpenOneMoreCase()
        {
            YandexGame.RewVideoShow(1);
        }

        public void SetLeaderboardScore(int score)
        {
            if (!YandexGame.SDKEnabled) return;
            
            YandexGame.NewLeaderboardScores("score", score);
        }
        
        public void SaveProgress()
        {
            if (!YandexGame.SDKEnabled) return;

            YandexGame.savesData.SaveData = _saveFeature.SaveData;
            YandexGame.SaveProgress();
        }
        
        public void GetSaves()
        {
            if (!YandexGame.SDKEnabled) return;
            
            if (YandexGame.savesData.SaveData == null)
                YandexGame.savesData.SaveData = _saveFeature.DefaultSaveData;
            else 
                _saveFeature.SaveData = YandexGame.savesData.SaveData;
        }
        
        private void GetRewarded(int id)
        {
            switch (id)
            {
                case 0:
                    _windowFactory.ReviveWindow.GetRevive();
                    break;
                case 1:
                    _windowFactory.OpenCaseWindow.GetOpenOneMore();
                    break;
            }
        }

        private Language ConvertYandexLanguageType()
        {
            Language language = Language.Russian;
            
            switch (YandexGame.EnvironmentData.language)
            {
                case "ru":
                    language = Language.Russian;
                    break;
                case "en":
                    language = Language.English;
                    break;
                case "ja":
                    language = Language.Japanese;
                    break;
            }
            return language;
        }

        private void SetGraphicsByDevice()
        {
            switch (_deviceFeature.CurrentDeviceType)
            {
                case DeviceType.Keyboard:
                    _settingsFeature.ChangeGraphics(GraphicsType.Medium);
                    break;
                case DeviceType.Mobile:
                    _settingsFeature.ChangeGraphics(GraphicsType.Low);
                    break;
            }
        }
        
        private void DetectDevice()
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                _deviceFeature.CurrentDeviceType = DeviceType.Mobile;
            }
            else
            {
                _deviceFeature.CurrentDeviceType = DeviceType.Keyboard;
            }
        }
    }
}