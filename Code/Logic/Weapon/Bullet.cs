using _GAME.Code.Factories;
using _GAME.Code.Logic.Character.Stats;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Weapon
{
    public class Bullet : MonoBehaviour
    {
        public Rigidbody Rb;
        [ReadOnly] public int Damage;
        public float FlyForce;
        public LayerMask DamageToLayer;
        public ParticleSystem DestroyEffect;

        [Inject] private VfxFactory _vfxFactory;
        
        private void OnTriggerEnter(Collider collider)
        {
            if (DamageToLayer != (DamageToLayer | (1 << collider.gameObject.layer))) return;

            StatsBase stats = collider.GetComponentInParent<StatsBase>();
            stats.TakeDamage(Damage, transform);
            
            // _vfxFactory.SpawnVfx(DestroyEffect, transform.position, Quaternion.identity, 2f);
            
            Destroy(gameObject);
        }
    }
}