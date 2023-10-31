using System.Collections.Generic;
using _GAME.Code.Logic.Enemy;
using _GAME.Code.Logic.Player;
using _GAME.Code.Logic.Spawners;
using Cinemachine;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace _GAME.Code.Logic.Level
{
    [ExecuteInEditMode]
    public class LevelRef : MonoBehaviour
    {
        public PlayerSpawner PlayerSpawner;
        [Space] 
        
        public Camera Camera;
        public CinemachineVirtualCamera VirtualCamera;

        [Header("Enemies")] 
        public List<EnemySpawner> EnemySpawners;
        public ReactiveCollection<EnemyRef> SpawnedEnemiesList = new();

        [Header("Chest")] 
        public Chest Chest;

        private void Start()
        {
            SpawnedEnemiesObserve();
        }

        [Button]
        private void SetUp()
        {
            EnemySpawners = new (FindObjectsOfType<EnemySpawner>());
            Chest = FindObjectOfType<Chest>();
        }

        private void SpawnedEnemiesObserve()
        {
            SpawnedEnemiesList.ObserveCountChanged().Subscribe(_ =>
            {
                if (SpawnedEnemiesList.Count == 0)
                {
                    Chest.EnableInteraction();
                }
            });
        }
    }
}