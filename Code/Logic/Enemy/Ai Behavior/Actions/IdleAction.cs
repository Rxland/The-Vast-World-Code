using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class IdleAction : EnemyAiBehaviorAction
    {
        public override void OnStart()
        {
            EnemyRef.AnimController.SetAnimBool(AnimName.Run, false);
            EnemyRef.AnimController.SetAnimBool(AnimName.Idle, true);
        }
    }
}