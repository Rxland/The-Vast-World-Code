using _GAME.Code.Factories;
using _GAME.Code.Logic.Game_State_Machine;
using _GAME.Code.Logic.Level;
using _GAME.Code.Types;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class LevelFeature : MonoBehaviour
    {
        public GameObject MainLevelHideOjbectsContainer;
        [Space]
        
        public LevelRef level;

        [Inject] private SaveFeature _saveFeature;
        [Inject] private EnvFactory _envFactory;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        [Inject] private SDKFeature _SDKFeature;
        

        public int GetCurrentLevelId()
        {
            return _saveFeature.SaveData.CurrentLevel;
        }

        
        public void SetNextLevelSaves()
        {
            if (!_saveReactivePropertiesFeature.FirstLevelDone.Value)
                _saveFeature.SaveData.InventorySaveData.SelectedItems.Clear();
            
            _saveReactivePropertiesFeature.FirstLevelDone.Value = true;
            _saveReactivePropertiesFeature.LevelsDoneAmount.Value++;
            _saveReactivePropertiesFeature.CurrentLevel.Value++;
            
            _SDKFeature.SetLeaderboardScore(_saveReactivePropertiesFeature.LevelsDoneAmount.Value);
        }
        
        public void RestartLevel()
        {
            StartCoroutine(_envFactory.UnLoadLLevel(GetCurrentLevelId(), () =>
            {
                _gameStateMachine.ChangeState(GameStateType.LoadLevel);
            }));
        }
        
        public void LoadNextLevel()
        {
            StartCoroutine(_envFactory.UnLoadLLevel(GetCurrentLevelId() - 1, () =>
            {
                _gameStateMachine.ChangeState(GameStateType.LoadLevel);
            }));
        }
    }
}