using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME.Code.Logic.Character
{
    public class CharacterMoveRef : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent NavMeshAgent;
        [Space]
        
        [ReadOnly] public Transform MoveToTarget;

        [ReadOnly] public float WalkSpeed;
        [ReadOnly] public float RotationSpeed;
        [ReadOnly] public float ArriveDistance;

        public void Init()
        {
            NavMeshAgent.speed = WalkSpeed;
            NavMeshAgent.angularSpeed = RotationSpeed;
            NavMeshAgent.stoppingDistance = ArriveDistance;
        }
    }
}