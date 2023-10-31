using System;
using _GAME.Code.Types;
using I2.Loc;

namespace _GAME.Code.Factories
{
    public class LocalizationFeature
    {
        public string GetTranslateByStatType(StatType statType)
        {
            string translation = String.Empty;
            
            switch (statType)
            {
                case StatType.Damage:
                    translation = LocalizationManager.GetTranslation("Stats/Damage");
                    break;
                case StatType.Hp:
                    translation = LocalizationManager.GetTranslation("Stats/Hp");
                    break;
                case StatType.AttackSpeed:
                    translation = LocalizationManager.GetTranslation("Stats/Attack Speed");
                    break;
            }
            return translation;
        }
    }
}