using _GAME.Code.Types;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class MoveToTargetAction : EnemyAiBehaviorAction
    {
        public EnemyRefSharedVariable EnemyRefSharedVariable;
        
        public SharedFloat angularSpeed = 120;
        public SharedFloat speed = 10;
        public SharedFloat arriveDistance = 0.2f;
        
        public SharedGameObject targetObject;

        private float _prevSpeed;
        private float _prevAngularSpeed;
        
        public override void OnStart()
        {
            EnemyRefSharedVariable.Value.AnimController.SetAnimBool(AnimName.Attack, false);
            EnemyRefSharedVariable.Value.AnimController.SetAnimBool(AnimName.Idle, false);
            EnemyRefSharedVariable.Value.AnimController.SetAnimBool(AnimName.Walk, false);
            EnemyRefSharedVariable.Value.AnimController.SetAnimBool(AnimName.Run, true);

            _prevSpeed = EnemyRef.Agent.speed;
            _prevAngularSpeed = EnemyRef.Agent.angularSpeed;
            
            EnemyRef.Agent.speed = speed.Value;
            EnemyRef.Agent.angularSpeed = angularSpeed.Value;
            
            SetDestination(Target());
        }

        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
                return TaskStatus.Success;
            
            SetDestination(Target());
            
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            
            EnemyRef.Agent.speed = _prevSpeed;
            EnemyRef.Agent.angularSpeed = _prevAngularSpeed;
            
            EnemyRefSharedVariable.Value.AnimController.SetAnimBool(AnimName.Run, false);
        }

        private Vector3 Target()
        {
            return targetObject.Value.transform.position;
        }
        
        private bool SetDestination(Vector3 destination)
        {
            EnemyRef.Agent.isStopped = false;
            return EnemyRef.Agent.SetDestination(destination);
        }
        
        private bool HasArrived()
        {
            float remainingDistance;
            
            if (EnemyRef.Agent.pathPending) {
                remainingDistance = float.PositiveInfinity;
            } else {
                remainingDistance = EnemyRef.Agent.remainingDistance;
            }

            return remainingDistance <= arriveDistance.Value;
        }
    }
}