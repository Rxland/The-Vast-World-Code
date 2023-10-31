using System.Collections.Generic;
using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Logic.Game_State_Machine
{
    public class GameStateMachine : MonoBehaviour
    {
        public GameStateBase CurrentState;
        [Space] 
        [SerializeField] private List<GameStateBase> _allGameStates;
    
        private void Awake()
        {
            CurrentState.OnEnter();
        }
        
        public void ChangeState(GameStateType gameStateType)
        {
            CurrentState.OnExit();

            CurrentState = GetGameStateByType(gameStateType);
            
            CurrentState.OnEnter();
        }
        
        private GameStateBase GetGameStateByType(GameStateType gameStateType)
        {
            foreach (GameStateBase gameStateBase in _allGameStates)
            {
                if (gameStateBase.StateType == gameStateType)
                    return gameStateBase;
            }
            return null;
        }
    }
}