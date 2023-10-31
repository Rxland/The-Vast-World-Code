using _GAME.Code.Features;
using _GAME.Code.Types;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Game_State_Machine
{
    public abstract class GameStateBase : MonoBehaviour
    {
        public GameStateType StateType;

        [HideInInspector] [Inject] public StaticDataFeature StaticDataFeature; 
        [HideInInspector] [Inject] public SaveFeature SaveFeature;
        [HideInInspector] [Inject] public SettingsFeature SettingsFeature;
        [HideInInspector] [Inject] public GameStateMachine GameStateMachine;

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}