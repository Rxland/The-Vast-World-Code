using _GAME.Code.Factories;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.Logic
{
    public class DragCameraController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _imageZone;
        
        private Vector2 touchStartPos;
        private Vector2 touchDelta;
        
        private bool _isInsideImageArea;
        
        [Inject] private GameFactory _gameFactory;

        private Vector3 lastMousePosition;
        private Vector3 deltaPosition;

        private Vector2 previousLocalPoint;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_imageZone.rectTransform, eventData.position,
                    eventData.pressEventCamera, out var localPoint))
            {
                localPoint = new Vector2(localPoint.x, -localPoint.y);
                
                if (previousLocalPoint == Vector2.zero)
                {
                    // If it's the first frame, set previousLocalPoint to the current localPoint
                    previousLocalPoint = localPoint;
                }
                
                Vector2 deltaPos = localPoint - previousLocalPoint;

                previousLocalPoint = localPoint;
                
                _gameFactory.Player.PlayerInputController.LookInput(deltaPos);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _gameFactory.Player.PlayerInputController.LookInput(Vector2.zero);
            previousLocalPoint = Vector2.zero;
        }
    }
}