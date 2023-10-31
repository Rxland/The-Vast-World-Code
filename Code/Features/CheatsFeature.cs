using System;
using System.Collections.Generic;
using _GAME.Code.Factories;
using _GAME.Code.Logic.Enemy;
using _GAME.Code.Types;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using YG;
using Zenject;

namespace _GAME.Code.Features
{
    public class CheatsFeature : MonoBehaviour
    {
        [Inject] private GameFactory _gameFactory;
        [Inject] private EnvFactory _envFactory;
        [Inject] private SaveFeature _saveFeature;

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.X))
        //     {
        //         _gameFactory.Player.PlayerAttackController.SetAttackSpeed(10f);
        //     }
        // }

        // [Button]
        // public void SetStunAnim()
        // {
        //     _gameFactory.Player.AnimController.SetAnimTrigger(AnimName.Stunned);
        // }
        
        
        [Button]
        public void ResetSaves()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.savesData.SaveData = _saveFeature.DefaultSaveData;
            YandexGame.SaveProgress();
        }
        
        [Button]
        private void KillAllEnemies()
        {
            List<EnemyRef> allEnemyRefs = new List<EnemyRef>(FindObjectsOfType<EnemyRef>());
            
            for (int i = 0; i < allEnemyRefs.Count; i++)
            {
                EnemyRef enemyRef = allEnemyRefs[i];
                
                enemyRef.statsBase.Kill();
            }
        }

        [Button]
        private void TeleportPlayerToChest()
        {
            _gameFactory.Player.transform.position = _envFactory.Level.Chest.transform.position;
        }

        [Button]
        private void KillPlayer()
        {
            _gameFactory.Player.Stats.Kill();
        }
    }
}