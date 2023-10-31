using _GAME.Code.Logic.Enemy;
using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Enemy Static Data", menuName = "Static Data/Enemy/Enemy Static Data")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyName EnemyName;
        public EnemyRef EnemyRefPrefab;

        [Header("Set Up")] 
        public int Hp;
        public int HitAmountToStun;
        public float WalkSpeed;
        public float RunSpeed;
        public float RotationSpeed;
        public float ArriveDistance;
        public int Damage;
        public float StunDuration;
        public float KillDelay = 1f;
        
        [Header("Editor")]
        public Mesh EnemyMesh;
        public float EnemyMeshScale = 1f;
        public Quaternion MeshRotation;
    }
}