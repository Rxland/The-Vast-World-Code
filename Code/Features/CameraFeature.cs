using Cinemachine;
using UnityEngine;

namespace _GAME.Code.Features
{
    public class CameraFeature : MonoBehaviour
    {
        public Camera Camera;
        public CinemachineVirtualCamera VirtualCamera;
        [Space]
        
        public float CameraFarClipPlane;

        public void UpdateFarClipPlane()
        {
            VirtualCamera.m_Lens.FarClipPlane = CameraFarClipPlane;
        }
    }
}