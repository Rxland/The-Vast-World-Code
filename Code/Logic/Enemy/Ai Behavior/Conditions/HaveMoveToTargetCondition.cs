using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class HaveMoveToTargetCondition : EnemyAiBehaviorCondition
    {
        public override TaskStatus OnUpdate()
        {
            if (EnemyRef.CharacterMoveRef.MoveToTarget)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}