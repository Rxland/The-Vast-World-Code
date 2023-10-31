using _GAME.Code.Types;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class EnemyStunnedAction : EnemyAiBehaviorAction
    {
        public SharedFloat StunDuration;
        public SharedBool IsStunned;
        
        private float waitDuration;
        private float startTime;
        private float pauseTime;
        
        public override void OnStart()
        {
            startTime = Time.time;
            waitDuration = StunDuration.Value;
            
            EnemyRef.AnimController.SetAnimBool(AnimName.Stunned, true);
        }

        public override TaskStatus OnUpdate()
        {
            if (startTime + waitDuration < Time.time) {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            IsStunned.Value = false;
            EnemyRef.AnimController.SetAnimBool(AnimName.Stunned, false);
        }
        
        public override void OnPause(bool paused)
        {
            if (paused) 
            {
                pauseTime = Time.time;
            } 
            else 
            {
                startTime += (Time.time - pauseTime);
            }
        }
    }
}