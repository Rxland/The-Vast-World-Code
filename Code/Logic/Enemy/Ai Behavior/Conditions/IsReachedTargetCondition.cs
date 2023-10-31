using _GAME.Code.Tools;
using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class IsReachedTargetCondition : EnemyAiBehaviorCondition
    {
        public override TaskStatus OnUpdate()
        {
            if (EnemyRef.CharacterMoveRef.MoveToTarget && GameExtensions.CheckIfAgentReachedDestination(EnemyRef.Agent))
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
    }
}