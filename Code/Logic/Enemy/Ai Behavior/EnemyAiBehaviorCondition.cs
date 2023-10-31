using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior
{
    public class EnemyAiBehaviorCondition : Conditional
    {
        public EnemyRef EnemyRef;
        
        public override void OnAwake()
        {
            base.OnAwake();
            EnemyRef = GetComponent<EnemyRef>();
        }
    }
}