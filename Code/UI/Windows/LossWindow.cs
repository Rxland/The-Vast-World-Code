using _GAME.Code.Features;
using _GAME.Code.UI.Buttons;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class LossWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private SimpleButton _restartButton;

        [Inject] private LevelFeature _levelFeature;
        
        public override void OpenWindow()
        {
            base.OpenWindow();

            Vector3 defaultRestartButtonScale = _restartButton.transform.localScale;
            
            _restartButton.transform.transform.localScale = Vector3.zero;
            
            _restartButton.Button.onClick.AddListener(OnRestartButtonClicked);
            
            DOTween.Sequence()
                .Append(_titleText.DOFade(0f, 0f))
                .Append(_titleText.DOFade(1f, 1f))
                .Append(_restartButton.transform.DOScale(defaultRestartButtonScale, 0.3f).SetEase(Ease.OutBack));
        }

        private void OnRestartButtonClicked()
        {
            TryHideWindows();
            
            _levelFeature.RestartLevel();
            CloseWindow();
        }
        
        private void TryHideWindows()
        {
            if (WindowFactory.HudWindow)
                WindowFactory.HudWindow.CloseWindow();
            
            if (WindowFactory.MobileControlsWindow)
                WindowFactory.MobileControlsWindow.CloseWindow();
        }
    }
}