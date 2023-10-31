using System;
using _GAME.Code.Tools;

namespace _GAME.Code.Save_Data
{
    [Serializable]
    public class SettingsSaveData
    {
        public float MainVolume;
        public Language LanguageType;
        public GraphicsType GraphicsType;
    }
}