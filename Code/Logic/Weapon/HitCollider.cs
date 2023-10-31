using System;
using System.Collections.Generic;
using _GAME.Code.Logic.Character;
using _GAME.Code.Logic.Character.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Logic.Weapon
{
    public class HitCollider : MonoBehaviour
    {
        public CapsuleCollider Collider;
        [SerializeField] public Weapon _weapon;
        [ReadOnly] [ShowInInspector] private List<Collider> _hittedColliders = new();
        
        private void OnTriggerEnter(Collider collider)
        {
            if (_weapon.HitLayerMask != (_weapon.HitLayerMask | (1 << collider.gameObject.layer)) || _hittedColliders.Contains(collider)) return;

            if (collider.TryGetComponent(out StatsBase stats))
            {
                stats.TakeDamage(_weapon.Damage, _weapon.OwnerTransform);
                
                _hittedColliders.Add(collider);
            }
        }
        
        public void TurnOnHitCollider()
        {
            Collider.enabled = true;
        }
        public void TurnOffHitCollider()
        {
            _hittedColliders.Clear();
            
            Collider.enabled = false;
        }
    }
}