using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class HaveAttackTargetCondition : EnemyAiBehaviorCondition
    {
        public SharedGameObject TargetObject;
        
        public override TaskStatus OnUpdate()
        {
            if (TargetObject.Value != null)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}