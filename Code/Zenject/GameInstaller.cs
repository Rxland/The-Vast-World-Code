using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Game_State_Machine;
using Zenject;

namespace _GAME.Code.Zenject
{
    public class GameInstaller : MonoInstaller
    {
        public SaveFeature SaveFeature;
        public CameraFeature CameraFeature;
        public LevelFeature LevelFeature;
        public WindowFactory WindowFactory;
        public GameStateMachine GameStateMachine;
        public RunTimeDataFeature RunTimeDataFeature;
        public SaveReactivePropertiesFeature saveReactivePropertiesFeature;
        public DeviceFeature DeviceFeature;
        public SDKFeature SDKFeature;
        
        public override void InstallBindings()
        {
            Container.Bind<StaticDataFeature>().AsSingle().NonLazy();
            Container.Bind<CharacterFeature>().AsSingle().NonLazy();
            Container.Bind<PlayerFeature>().AsSingle().NonLazy();
            Container.Bind<SettingsFeature>().AsSingle().NonLazy();
            Container.Bind<MoneyFeature>().AsSingle().NonLazy();
            Container.Bind<InventoryFeature>().AsSingle().NonLazy();
            Container.Bind<TutorialFeature>().AsSingle().NonLazy();
            Container.Bind<CursorFeature>().AsSingle().NonLazy();
            Container.Bind<NGplusFeature>().AsSingle().NonLazy();
            Container.Bind<LocalizationFeature>().AsSingle().NonLazy();
            
            Container.Bind<VfxFactory>().AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<GameFactory>().AsSingle().NonLazy();
            Container.Bind<EnvFactory>().AsSingle().NonLazy();
            Container.Bind<UiFactory>().AsSingle().NonLazy();
            Container.Bind<SoundFactory>().AsSingle().NonLazy();
            
            Container.Bind<SaveFeature>().FromInstance(SaveFeature).AsSingle().NonLazy();
            Container.Bind<CameraFeature>().FromInstance(CameraFeature).AsSingle().NonLazy();
            Container.Bind<LevelFeature>().FromInstance(LevelFeature).AsSingle().NonLazy();
            Container.Bind<RunTimeDataFeature>().FromInstance(RunTimeDataFeature).AsSingle().NonLazy();
            Container.Bind<SaveReactivePropertiesFeature>().FromInstance(saveReactivePropertiesFeature).AsSingle().NonLazy();
            Container.Bind<DeviceFeature>().FromInstance(DeviceFeature).AsSingle().NonLazy();
            Container.Bind<SDKFeature>().FromInstance(SDKFeature).AsSingle().NonLazy();
            
            Container.Bind<WindowFactory>().FromInstance(WindowFactory).AsSingle().NonLazy();
            Container.Bind<GameStateMachine>().FromInstance(GameStateMachine).AsSingle().NonLazy();
        }
    }
}
