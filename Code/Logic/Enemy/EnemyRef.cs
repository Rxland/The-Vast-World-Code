using _GAME.Code.Factories;
using _GAME.Code.Logic.Character;
using _GAME.Code.Logic.Character.Stats;
using _GAME.Code.Logic.Player;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _GAME.Code.Logic.Enemy
{
    public class EnemyRef : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public StatsBase statsBase;
        public AnimController AnimController;
        public CharacterAttackController CharacterAttackController;
        public CharacterMoveRef CharacterMoveRef;
        public BehaviorTree BehaviorTree;
        
        [Inject] public VfxFactory VfxFactory;
    }
    
    [System.Serializable]
    public class EnemyRefSharedVariable : SharedVariable<EnemyRef>
    {
        public static implicit operator EnemyRefSharedVariable(EnemyRef value) { return new EnemyRefSharedVariable { Value = value }; }
    }
}