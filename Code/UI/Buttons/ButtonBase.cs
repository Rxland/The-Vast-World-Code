using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Code.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonBase : MonoBehaviour
    {
        public Button Button;

        protected virtual void OnDisable()
        {
            ButtonReset();
        }
        
        protected virtual void ButtonReset()
        {
            Button.onClick.RemoveAllListeners();
        }
        
        private void Reset()
        {
            Button = GetComponent<Button>();
        }
    }
}