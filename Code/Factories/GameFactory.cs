using _GAME.Code.Factories.Pools;
using _GAME.Code.Features;
using _GAME.Code.Logic;
using _GAME.Code.Logic.Player;
using _GAME.Code.Logic.Weapon;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _GAME.Code.Factories
{
    public class GameFactory
    {
        public PlayerRef Player => _player;
        private PlayerRef _player;
        
        [Inject] private DiContainer _diContainer;
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private EnvFactory _envFactory;
        [Inject] private CameraFeature _cameraFeature;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private CoinsPool _coinsPool;
        // [Inject] private CoinsFactory _coinsFactory;
        
        
        public void SpawnPlayer()
        {
            _player = _diContainer.InstantiatePrefabForComponent<PlayerRef>(_staticDataFeature.PlayerStaticData.PlayerRefPrefab);
            SceneManager.MoveGameObjectToScene(_player.gameObject, SceneManager.GetActiveScene());
            
            _player.transform.position = _envFactory.Level.PlayerSpawner.transform.position;

            _player.Stats.HpBarUi = _windowFactory.HudWindow.HpBarUi;
            
            _cameraFeature.VirtualCamera.Follow = _player.CameraRoot;
        }

        public Bullet SpawnBullet(Bullet bulletPrefab, Transform spawnPoint)
        {
            Bullet newBullet = _diContainer.InstantiatePrefabForComponent<Bullet>(bulletPrefab);
            newBullet.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

            return newBullet;
        }

        public void SpawnCoins(Vector3 spawnPos, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                SpawnCoin(spawnPos);
            }
        }

        private void SpawnCoin(Vector3 spawnPos)
        {
            Coin newCoin = _coinsPool.Spawn();
                newCoin.gameObject.SetActive(true);
            
            newCoin.transform.position = spawnPos;
            
            newCoin.Init();
        }
    }
}