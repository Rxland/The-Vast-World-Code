using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class WindowBase : MonoBehaviour
    {
        public WindowName WindowName;
        public SimpleButton BackButton;
        
        [HideInInspector] [Inject] public WindowFactory WindowFactory;
        [HideInInspector] [Inject] public CursorFeature CursorFeature;
        
        protected virtual void OnEnable()
        {
            if (BackButton)
            {
                BackButton.Button.onClick.RemoveAllListeners();
                BackButton.Button.onClick.AddListener(() => CloseWindow());
            }
        }

        public virtual void OpenWindow()
        {
            CursorFeature.SetCursorLockedMode(false);
        }

        public virtual void CloseWindow()
        {
            CursorFeature.SetCursorLockedMode(true);
            Destroy(gameObject);
        }

        public virtual void ReOpenWindow()
        {
            int siblingId = transform.GetSiblingIndex();
            
            CloseWindow();
            
            WindowBase windowBase = WindowFactory.SpawnWindow(WindowName);
            
            windowBase.transform.SetSiblingIndex(siblingId);
        }
    }
}