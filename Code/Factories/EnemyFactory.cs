using _GAME.Code.Features;
using _GAME.Code.Logic.Enemy;
using _GAME.Code.Logic.Spawners;
using _GAME.Code.Static_Data;
using _GAME.Code.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _GAME.Code.Factories
{
    public class EnemyFactory
    {
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private DiContainer _diContainer;
        [Inject] private LevelFeature _levelFeature;
        [Inject] private GameFactory _gameFactory;
        [Inject] private NGplusFeature _NGplusFeature;

        public EnemyRef SpawnEnemyByName(EnemyName enemyName, Vector3 spawnPos, Quaternion spawnRot, EnemySpawner enemySpawner)
        {
            EnemyStaticData enemyStaticData = _staticDataFeature.GetEnemyByName(enemyName);
            EnemyRef enemyRef = _diContainer.InstantiatePrefabForComponent<EnemyRef>(enemyStaticData.EnemyRefPrefab);
            SceneManager.MoveGameObjectToScene(enemyRef.gameObject, SceneManager.GetActiveScene());
            
            enemyRef.Agent.Warp(spawnPos);
            enemyRef.transform.rotation = spawnRot;
            
            enemyRef.statsBase.MaxHp = _NGplusFeature.GetNGenemyHp(enemyStaticData.Hp);
            enemyRef.statsBase.MaxHitAmountToStun = enemyStaticData.HitAmountToStun;
            enemyRef.statsBase.KillDelay = enemyStaticData.KillDelay;

            enemyRef.CharacterMoveRef.WalkSpeed = enemyStaticData.WalkSpeed;
            enemyRef.CharacterMoveRef.RotationSpeed = enemyStaticData.RotationSpeed;
            enemyRef.CharacterMoveRef.ArriveDistance = enemyStaticData.ArriveDistance;

            enemyRef.CharacterAttackController.Weapon.Damage = enemyStaticData.Damage;
            enemyRef.CharacterAttackController.Damage = enemyStaticData.Damage;
            
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.PatrolPoints.ToString(), enemySpawner.PatrolPoints);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.AttackTarget.ToString(), _gameFactory.Player.gameObject);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.WalkSpeed.ToString(), enemyStaticData.WalkSpeed);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.RunSpeed.ToString(), enemyStaticData.RunSpeed);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.AngularSpeed.ToString(), enemyStaticData.RotationSpeed);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.ArriveDistance.ToString(), enemyStaticData.ArriveDistance);
            enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.StunDuration.ToString(), enemyStaticData.StunDuration);

            enemyRef.Agent.angularSpeed = enemyStaticData.RotationSpeed;
            
            enemyRef.statsBase.Init();
            enemyRef.CharacterMoveRef.Init();
            
            _levelFeature.level.SpawnedEnemiesList.Add(enemyRef);
            
            return enemyRef;
        }
    }
}