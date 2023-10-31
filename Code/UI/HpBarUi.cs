using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Code.UI
{
    public class HpBarUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpAmountText;
        [Space]
        
        [SerializeField] private Slider _mainBarSlider;
        [SerializeField] private Slider _secondBarSlider;
        [Space]
        
        [SerializeField] private Image _mainBarFilled;
        [SerializeField] private Image _secondBarFilled;
        [Space]
        
        [SerializeField] private float _secondBarFilledLerpValue;
        [SerializeField] private float _secondBarFilledLerpDelay;
        
        
        public void SetBarFilled(float maxHp, float currentHp)
        {
            if (_mainBarSlider)
            {
                _mainBarSlider.value = currentHp / maxHp;
                _secondBarSlider.DOValue(currentHp / maxHp, _secondBarFilledLerpValue).SetDelay(_secondBarFilledLerpDelay);
            }
            else
            {
                _mainBarFilled.fillAmount = currentHp / maxHp;
                _secondBarFilled.DOFillAmount(currentHp / maxHp, _secondBarFilledLerpValue).SetDelay(_secondBarFilledLerpDelay);
            }
            
            if (_hpAmountText)
            {
                _hpAmountText.text = $"{currentHp}";
            }

        }
    }
}