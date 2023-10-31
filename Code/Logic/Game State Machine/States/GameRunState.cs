using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Spawners;
using Zenject;

namespace _GAME.Code.Logic.Game_State_Machine.States
{
    public class GameRunState : GameStateBase
    {
        [Inject] private CursorFeature _cursorFeature;
        [Inject] private GameFactory _gameFactory;
        [Inject] private EnvFactory _envFactory;
        [Inject] private CameraFeature _cameraFeature;
        
        public override void OnEnter()
        {
            _cursorFeature.SetCursorLockedMode(true);
            
            _gameFactory.SpawnPlayer();
            _gameFactory.Player.Stats.Init();
            _gameFactory.Player.PlayerAttackController.Init();
            
            SpawnEnemies();
            
            _cameraFeature.UpdateFarClipPlane();
        }

        public override void OnExit()
        {
            
        }
        
        private void SpawnEnemies()
        {
            foreach (EnemySpawner levelEnemySpawner in _envFactory.Level.EnemySpawners)
            {
                levelEnemySpawner.OnEnter();
            }
        }
    }
}