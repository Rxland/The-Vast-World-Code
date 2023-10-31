using _GAME.Code.Features;
using _GAME.Code.Types;
using Zenject;

namespace _GAME.Code.Logic.Game_State_Machine.States
{
    public class LoadGameState : GameStateBase
    {
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        
        public override void OnEnter()
        {
            StaticDataFeature.Init();
            SettingsFeature.InitGameSettings();
            
            GameStateMachine.ChangeState(GameStateType.LoadMainMenu);
        }

        public override void OnExit()
        {
            
        }
    }
}