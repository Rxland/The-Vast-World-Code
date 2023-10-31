
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class HavePatrolPoints : EnemyAiBehaviorCondition
    {
        public SharedGameObjectList PatrolPoints;
        
        public override TaskStatus OnUpdate()
        {
            if (PatrolPoints.Value.Count > 0)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}