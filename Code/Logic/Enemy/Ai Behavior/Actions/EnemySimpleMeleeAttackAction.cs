using _GAME.Code.Types;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class EnemySimpleMeleeAttackAction : EnemyAiBehaviorAction
    {
        public override void OnStart()
        {
            EnemyRef.AnimController.SetAnimBool(AnimName.Run, false);
            EnemyRef.AnimController.SetAnimBool(AnimName.Idle, false);
            EnemyRef.AnimController.SetAnimBool(AnimName.Attack, true);
        }
    }
}