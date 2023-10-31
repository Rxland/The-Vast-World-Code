using System;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class MoneyFeature
    {
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        
        public void IncreaseMoney(int amount)
        {
            _saveReactivePropertiesFeature.Money.Value += amount;
        }
        
        public void DecreaseMoney(int amount)
        {
            _saveReactivePropertiesFeature.Money.Value = Mathf.Clamp(_saveReactivePropertiesFeature.Money.Value - amount, 0, Int32.MaxValue);
        }

        public bool HaveMoneyByBuy(int needMoneyValue)
        {
            if (_saveReactivePropertiesFeature.Money.Value < needMoneyValue)
                return false;

            return true;
        }
    }
}