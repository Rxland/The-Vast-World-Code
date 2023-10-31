using _GAME.Code.Types;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace _GAME.Code.Logic.Weapon
{
    public class RangeWeapon : Weapon
    {
        public Bullet BulletPrefab;
        public BehaviorTree BehaviorTree;
        public Transform ShootTarget;
        [SerializeField] private Transform _bulletSpawnPoint;
        
        public override void Shoot()
        {
            base.Shoot();
            
            GameObject shootTargetGo = (GameObject)BehaviorTree.GetVariable(BotBehaviorVariableName.AttackTarget.ToString()).GetValue();
            ShootTarget = shootTargetGo.transform;
            
            if (!ShootTarget)
                return;
            
            Bullet spawnedBullet = GameFactory.SpawnBullet(BulletPrefab, _bulletSpawnPoint);
            spawnedBullet.Damage = Damage;
            
            Vector3 direction = (ShootTarget.position - _bulletSpawnPoint.position).normalized;
            Vector3 targetVelocity = Vector3.zero;

            float timeToReachTarget = Vector3.Distance(ShootTarget.position, _bulletSpawnPoint.position) / spawnedBullet.FlyForce;
            
            if (shootTargetGo.TryGetComponent(out CharacterController characterController))
            {
                targetVelocity = characterController.velocity;
            }
            
            Vector3 predictedTargetPosition = ShootTarget.position + (targetVelocity * timeToReachTarget);

            direction = (predictedTargetPosition - _bulletSpawnPoint.position).normalized;
            
            spawnedBullet.Rb.AddForce(direction * spawnedBullet.FlyForce, ForceMode.Impulse);
            
            Destroy(spawnedBullet.gameObject ? spawnedBullet.gameObject: null, 10);
        }
    }
}