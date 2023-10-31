using System.Collections.Generic;
using System.Linq;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Enemy;
using _GAME.Code.Logic.Extentions;
using _GAME.Code.Logic.Interaction.Dynamic;
using _GAME.Code.Static_Data;
using _GAME.Code.Tools;
using _GAME.Code.Types;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Spawners
{
    [ExecuteInEditMode]
    public class EnemySpawner : DynamicInteractableBase
    {
        public EnemyName EnemyName;
        public UniqueId UniqueId;
        private EnemyRef _spawnedEnemyRef;
        public List<GameObject> PatrolPoints;

        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private CharacterFeature _characterFeature;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;


        public override void OnEnter()
        {
            if (_characterFeature.IsCharacterKilled(UniqueId.Id))
                return;

            if (_saveReactivePropertiesFeature.NGplusLevel.Value > 0)
            {
                EnemyName = GameExtensions.GetRandomEnumValue<EnemyName>();
            }
            
            _spawnedEnemyRef = _enemyFactory.SpawnEnemyByName(EnemyName, transform.position, transform.rotation, this);
        }

        public override void OnExit()
        {
            if (!_spawnedEnemyRef) return;
            
            Destroy(_spawnedEnemyRef.gameObject);
        }
        
        #region Editor Only
        
        private const string _enemyStaticDataPath = "Static Data/Enemy";
        private List<EnemyStaticData> _enemyStaticDataList;
        
        private void OnDrawGizmos()
        {
            _enemyStaticDataList = Resources.LoadAll<EnemyStaticData>(_enemyStaticDataPath).ToList();
            EnemyStaticData enemyStaticData = GetEnemyByName(EnemyName);
            
            if (enemyStaticData.EnemyMesh != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawMesh(enemyStaticData.EnemyMesh, transform.position,  transform.localRotation * enemyStaticData.MeshRotation, Vector3.one * enemyStaticData.EnemyMeshScale);
            }
        }
        
        
        private EnemyStaticData GetEnemyByName(EnemyName enemyName)
        {
            foreach (EnemyStaticData enemyStaticData in _enemyStaticDataList)
            {
                if (enemyStaticData.EnemyName == enemyName)
                    return enemyStaticData;
            }
            return null;
        }
        #endregion
    }
}