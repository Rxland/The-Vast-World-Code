using _GAME.Code.Types;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class EnemyResetAttackAction : EnemyAiBehaviorAction
    {
        public override void OnStart()
        {
            EnemyRef.CharacterAttackController.ResetAttack();
        }
    }
}