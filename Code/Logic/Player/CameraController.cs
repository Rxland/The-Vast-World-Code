using _GAME.Code.Features;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerRef _playerRef;

        [Inject] private CameraFeature _cameraFeature;

        private void Update()
        {
            if (!_cameraFeature.Camera) return;
            
            Vector3 cameraForward = _cameraFeature.Camera.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward, transform.up);
            Vector3 originalVector = new Vector3(0f, targetRotation.eulerAngles.y, 0f);

            _playerRef.CharacterVariables.CharacterCameraForward = originalVector;
        }
    }
}