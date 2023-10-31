using _GAME.Code.Factories;
using Zenject;

namespace _GAME.Code.Logic.Game_State_Machine.States
{
    public class LoadMainMenuState : GameStateBase
    {
        [Inject] private WindowFactory _windowFactory;
        
        public override void OnEnter()
        {
            _windowFactory.CurrentWindow.OpenWindow();
        }

        public override void OnExit()
        {
            
        }
    }
}