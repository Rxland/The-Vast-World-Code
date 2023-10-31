using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using UnityEngine;

namespace _GAME.Code.Logic.Enemy.Ai_Behavior.Conditions
{
    public class CustomCanSeeObjectCondition : EnemyAiBehaviorCondition
    {
        public LayerMask enemyLayer;
        public SharedFloat detectionRadius = 10f;
        public SharedFloat detectionAngle = 45f;
        public SharedFloat checkInterval = 0.5f;

        private bool _canSeeObject;
        
        public override void OnStart()
        {
            base.OnStart();

            StartCoroutine(DetectEnemies());
        }

        public override TaskStatus OnUpdate()
        {
            if (_canSeeObject)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            StopCoroutine(DetectEnemies());
        }

        private IEnumerator DetectEnemies()
        {
            while (true)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius.Value, enemyLayer);

                foreach (var collider in colliders)
                {
                    Vector3 directionToEnemy = (collider.transform.position - transform.position).normalized;
                    float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                    if (angleToEnemy < detectionAngle.Value * 0.5f)
                    {
                        _canSeeObject = true;
                    }
                }

                yield return new WaitForSeconds(checkInterval.Value);
            }
        }
        
        public override void OnDrawGizmos()
        {
            MovementUtility.DrawLineOfSight(Owner.transform, Vector3.zero, detectionAngle.Value, 0f, detectionRadius.Value, false);
        }
    }
}