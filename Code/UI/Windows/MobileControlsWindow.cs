using _GAME.Code.Factories;
using _GAME.Code.UI.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class MobileControlsWindow : WindowBase
    {
        [SerializeField] private SimpleButton _attackButton;
        [SerializeField] private SimpleButton _dushButton;
        
        [Inject] private GameFactory _gameFactory;
        
        [Button]
        public override void OpenWindow()
        {
            base.OpenWindow();
            
            _attackButton.Button.onClick.AddListener(() => _gameFactory.Player.PlayerInputController.OnAttackEvent?.Invoke());
            _dushButton.Button.onClick.AddListener(() => _gameFactory.Player.PlayerInputController.OnForwardDodgeEvent?.Invoke());
        }
        
        public override void CloseWindow()
        {
            _attackButton.Button.onClick.RemoveAllListeners();
            _dushButton.Button.onClick.RemoveAllListeners();
            
            base.CloseWindow();
        }
        
    }
}