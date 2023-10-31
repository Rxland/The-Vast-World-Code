using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Types;
using _GAME.Code.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Character.Stats
{
    public abstract class StatsBase : MonoBehaviour
    {
        public int MaxHp;
        [ReadOnly] [ShowInInspector] public ReactiveProperty<int> CurrentHpRP = new();
        [Space]
        
        [ReadOnly] public bool IsKilled;

        [Header("Effects")] 
        public ParticleSystem HittedEffect;

        [Header("Stun")] 
        public int MaxHitAmountToStun = 1;
        [ReadOnly] public int CurrentHitAmountToStunFire;
        
        [Header("Hit")] 
        public float _hitMoveForce;
        public float _hitMoveDuration;
        public Ease _hitMoveEase;
        public float _hitPunchStrength;
        public float _hitPunchDuration;

        [Header("Kill")] 
        public float KillDelay;
        public ParticleSystem KillEffect;

        [Header("UI")] 
        public HpBarUi HpBarUi;
        
        [Header("Other")] 
        public Collider Collider;
        

        [Inject] private VfxFactory _vfxFactory;
        [HideInInspector] [Inject] public InventoryFeature InventoryFeature;
        [HideInInspector] [Inject] public SoundFactory SoundFactory;
        [HideInInspector] [Inject] public StaticDataFeature StaticDataFeature;
        
        
        public virtual void Init()
        {
            CurrentHpRP.Value = MaxHp;
            CurrentHitAmountToStunFire = MaxHitAmountToStun;
            
            CurrentHpRP.Subscribe(_ =>
            {
                HpBarUi?.SetBarFilled(MaxHp, CurrentHpRP.Value);
            });
        }
        
        public virtual void TakeDamage(int damage, Transform damageFromT)
        {
            if (IsKilled) return;
            
            CurrentHpRP.Value = Mathf.Clamp(CurrentHpRP.Value - damage, 0, int.MaxValue);

            Hit(damage, damageFromT);

            if (CurrentHpRP.Value <= 0)
            {
                Kill();
            }
        }

        protected virtual void Hit(int damage, Transform damageFromT)
        {
            SpawnHittedEffect();
            TryStun(damageFromT);
        }

        [Button]
        public virtual void Kill()
        {
            IsKilled = true;
        }

        [Button]
        public virtual void Revive()
        {
            CurrentHpRP.Value = MaxHp;
            IsKilled = false;
        }

        protected virtual bool TryStun(Transform damageFromT)
        {
            CurrentHitAmountToStunFire--;
            
            if (CurrentHitAmountToStunFire > 0)
            {
                return false;
            }
            
            CurrentHitAmountToStunFire = MaxHitAmountToStun;
            return true;
        }
        
        protected virtual void SpawnKilledEffect()
        {
            if (!KillEffect) return;
            
            _vfxFactory.SpawnPooledEffect(EffectName.DeadPuff, KillEffect.transform.position, KillEffect.transform.rotation);
        }

        protected virtual void SpawnHittedEffect()
        {
            if (!HittedEffect) return;
            
            _vfxFactory.SpawnPooledEffect(EffectName.Blood, HittedEffect.transform.position, HittedEffect.transform.rotation, 1f);
        }
    }
}