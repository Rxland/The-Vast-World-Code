using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Actions
{
    public class EnemyDetectionAction : EnemyAiBehaviorAction
    {
        public override void OnStart()
        {
            AttackTargetDetection();
        }
        
        private bool AttackTargetDetection()
        {
            if (EnemyRef.CharacterAttackController.IsDetectedAttackTarget) return true;
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, EnemyRef.CharacterAttackController.AttackDetectionRange, EnemyRef.CharacterAttackController.AttackLayerMask);

            if (colliders.Length == 0)
                return false;
            
            Collider nearestCollider = null;
            float nearestDistance = Mathf.Infinity;

            for (int i = 0; i < colliders.Length; i++)
            {
                Collider collider = colliders[i];
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < nearestDistance)
                {
                    nearestCollider = collider;
                    nearestDistance = distance;
                }
            }

            EnemyRef.CharacterMoveRef.MoveToTarget = nearestCollider.transform;
            return true;
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyRef.CharacterAttackController.AttackDetectionRange);
        }
    }
}