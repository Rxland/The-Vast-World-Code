using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Code.UI.Windows
{
    public class HudWindow : WindowBase
    {
        public HpBarUi HpBarUi;
        [SerializeField] private Image _blackImg;
        
        public override void OpenWindow()
        {
            base.OpenWindow();
            CursorFeature.SetCursorLockedMode(true);

            _blackImg.DOFade(1f, 0);
            _blackImg.DOFade(0f, 3f).SetDelay(1);
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
        }
    }
}