using _GAME.Code.Types;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class EnemyKilledAction : EnemyAiBehaviorAction
    {
        public override void OnStart()
        {
            base.OnStart();
            
            EnemyRef.AnimController.SetAnimTrigger(AnimName.Die);
        }
    }
}