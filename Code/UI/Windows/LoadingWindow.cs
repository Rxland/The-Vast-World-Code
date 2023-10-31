using UnityEngine.UI;

namespace _GAME.Code.UI.Windows
{
    public class LoadingWindow : WindowBase
    {
        public Slider LoadingSlider;
        
        public override void OpenWindow()
        {
            LoadingSlider.value = 0f;
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
        }
    }
}