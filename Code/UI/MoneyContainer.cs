using _GAME.Code.Features;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI
{
    public class MoneyContainer : MonoBehaviour
    {
        public TextMeshProUGUI MoneyAmountText;
        
        [Inject] private MoneyFeature _moneyFeature;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;

        private void Start()
        {
            _saveReactivePropertiesFeature.Money.Subscribe(_ =>
            {
                UpdateMoneyText(_saveReactivePropertiesFeature.Money.Value);
                
            }).AddTo(this);
        }
        
        private void UpdateMoneyText(int moneyAmount)
        {
            string formattedNumber = moneyAmount.ToString("N0"); // "N0" adds commas
            formattedNumber = formattedNumber.Replace(",", ".");

            MoneyAmountText.text = formattedNumber;
        }
    }
}