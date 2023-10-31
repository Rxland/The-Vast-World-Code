using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class IsEnemyKilledCondition : EnemyAiBehaviorCondition
    {
        public override TaskStatus OnUpdate()
        {
            if (EnemyRef.statsBase.IsKilled)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}