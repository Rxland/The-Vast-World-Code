using BehaviorDesigner.Runtime.Tasks;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior
{
    public class EnemyAiBehaviorAction : Action
    {
        public EnemyRef EnemyRef;
        
        public override void OnAwake()
        {
            base.OnAwake();
            EnemyRef = GetComponent<EnemyRef>();
        }
    }
}