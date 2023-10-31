using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Level;
using _GAME.Code.Types;
using Zenject;

namespace _GAME.Code.Logic.Game_State_Machine.States
{
    public class LoadLevelState : GameStateBase
    {
        [Inject] private EnvFactory _envFactory;
        [Inject] private CameraFeature _cameraFeature;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private LevelFeature _levelFeature;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        [Inject] private SDKFeature _sdkFeature;
        
        public override void OnEnter()
        {
            _envFactory.LevelLoadedAction += LevelLoaded;
            
            _windowFactory.SpawnWindow(WindowName.Loading);
            _levelFeature.MainLevelHideOjbectsContainer.gameObject.SetActive(false);
            
            if (_windowFactory.MainMenuWindow)
                _windowFactory.MainMenuWindow.CloseWindow();
            
            StartCoroutine(_envFactory.SpawnLevel(_levelFeature.GetCurrentLevelId()));
        }

        public override void OnExit()
        {
            _envFactory.LevelLoadedAction -= LevelLoaded;
        }

        private void LevelLoaded()
        {
            _windowFactory.SpawnWindow(WindowName.Hud);
            _windowFactory.TrySpawnMobileControlsWindow();
            
            _cameraFeature.Camera = _envFactory.Level.Camera;
            _cameraFeature.VirtualCamera = _envFactory.Level.VirtualCamera;

            _levelFeature.level = FindObjectOfType<LevelRef>();

            _windowFactory.LoadingWindow.CloseWindow();

            if (_saveReactivePropertiesFeature.FirstLevelDone.Value)
                _windowFactory.SpawnWindow(WindowName.Inventory);
            else
                GameStateMachine.ChangeState(GameStateType.GameRun);
            
            _sdkFeature.ShowFullScreen();
        }
    }
}