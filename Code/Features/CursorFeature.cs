using UnityEngine;

namespace _GAME.Code.Features
{
    public class CursorFeature
    {
        public void SetCursorLockedMode(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !newState;
        }
    }
}