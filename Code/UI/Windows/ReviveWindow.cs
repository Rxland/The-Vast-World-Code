using System.Collections;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Types;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class ReviveWindow : WindowBase
    {
        [SerializeField] private Image _filledBarImg;
        [SerializeField] private TextMeshProUGUI _timeToDieText;
        [Space] 
        
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _noThanksButton;
        
        [SerializeField] private int _timerDuration;

        [Inject] private GameFactory _gameFactory;
        [Inject] private SDKFeature _sdkFeature;
        
        public override void OpenWindow()
        {
            base.OpenWindow();

            Init();
            
            StartCoroutine(TimerToDieIE());
            StartCoroutine(StartNoThanksButtonAnimIE());
        }

        private void Init()
        {
            _noThanksButton.onClick.AddListener(OpenLossWindow);
            _reviveButton.onClick.AddListener(ReviveButtonClick);
        }
        
        private IEnumerator TimerToDieIE()
        {
            int fireT = _timerDuration;

            _filledBarImg.fillAmount = 0f;
            
            _filledBarImg.DOFillAmount(1f, _timerDuration);
            _timeToDieText.text = fireT.ToString();
            
            for (int i = 0; i < _timerDuration; i++)
            {
                _timeToDieText.text = $"{fireT}";
                
                yield return new WaitForSeconds(1f);

                _timeToDieText.transform.DOPunchScale(Vector3.one, 0.3f, 5);
                fireT -= 1;
            }
            OpenLossWindow();
        }

        private IEnumerator StartNoThanksButtonAnimIE()
        {
            _noThanksButton.transform.localScale = Vector3.zero;

            yield return new WaitForSeconds(2f);

            _noThanksButton.transform.DOScale(Vector3.one, 0.3f);
        }
        
        private void OpenLossWindow()
        {
            WindowFactory.SpawnWindow(WindowName.Loss);
            CloseWindow();
            CursorFeature.SetCursorLockedMode(false);
        }

        private void ReviveButtonClick()
        {
            _sdkFeature.ShowRewardVideoRevive();
        }
        
        public void GetRevive()
        {
            if (WindowFactory.LossWindow)
                WindowFactory.LossWindow.CloseWindow();                
            
            _gameFactory.Player.Stats.Revive();
            CursorFeature.SetCursorLockedMode(true);
            
            CloseWindow();
        }
    }
}