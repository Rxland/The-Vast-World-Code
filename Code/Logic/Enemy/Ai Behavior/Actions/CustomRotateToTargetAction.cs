using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class CustomRotateToTargetAction : EnemyAiBehaviorAction
    {
        public SharedFloat rotationSpeed;
        public SharedFloat rotationEpsilon = 0.5f;
        
        public SharedGameObject target;

        // public override TaskStatus OnUpdate()
        // {
        //     // Return a task status of success once we are done rotating
        //     if (Quaternion.Angle(transform.rotation, rotation) < rotationEpsilon.Value) {
        //         return TaskStatus.Success;
        //     }
        //     
        //     transform.DO
        // }
    }
}