using _GAME.Code.Features;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Tools
{
    public class RotateToCamera : MonoBehaviour
    {
        [Inject] private CameraFeature _cameraFeature;

        private void LateUpdate()
        {
            transform.forward = -_cameraFeature.Camera.transform.forward;
        }
    }
}