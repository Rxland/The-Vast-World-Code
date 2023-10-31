using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class NGplusFeature
    {
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        
        public int GetNGenemyHp(int enemyDefaultHp)
        {
            if (_saveReactivePropertiesFeature.NGplusLevel.Value == 0)
                return enemyDefaultHp;
            
            return Mathf.RoundToInt(_staticDataFeature.NGplusStaticData.EnemiesHpScaleByNGLevel * _saveReactivePropertiesFeature.NGplusLevel.Value * enemyDefaultHp);
        }
    }
}