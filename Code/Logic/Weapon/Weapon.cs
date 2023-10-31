using System.Collections.Generic;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Player;
using _GAME.Code.Static_Data;
using _GAME.Code.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public WeaponName WeaponName;
        public WeaponConfigData weaponConfigData;
        [Space]
        
        [Header("Damage")]
        [ReadOnly] public int Damage;
        [Space]
        
        public FlyWeapon FlyWeapon;
        public bool IsPlayer;
        public int MaxAnimStates;
        public HitCollider HitCollider;
        public LayerMask HitLayerMask;
        public Transform OwnerTransform;
        
        [SerializeField] private List<ParticleSystem> _slashEffects;
        [SerializeField] private List<DashMoveData> _dashMoveDataList;
        [SerializeField] private List<AudioSource> _slashSounds;

        [Inject] public GameFactory GameFactory;
        [Inject] public InventoryFeature InventoryFeature;
        [Inject] public SoundFactory SoundFactory;
        [Inject] public VfxFactory VfxFactory;


        public void Init()
        {
            Damage = Mathf.RoundToInt(InventoryFeature.GetAllEquippedStats(StatType.Damage));
            
            if (IsPlayer)
                ActivateWeapon(false);
        }

        public virtual void Shoot()
        {
            
        }
        
        public void PlayVfx(int id)
        {
            Effect effect = _slashEffects[id].GetComponent<Effect>();
            
            VfxFactory.SpawnPooledEffect(effect.EffectName, effect.transform.position, effect.transform.rotation);
            
            // _slashEffects[id].Play();
        }

        public void PlayRandomSlashSound()
        {
            SoundFactory.SpawnSound(_slashSounds[Random.Range(0, _slashSounds.Count)], OwnerTransform.position);
        }
        
        public DashMoveData GetDashMoveData(int id)
        {
            return _dashMoveDataList[id];
        }

        public void TurnOnWeaponHitCollider()
        {
            HitCollider.TurnOnHitCollider();
        }
        
        public void TurnOffWeaponHitCollider()
        {
            HitCollider.TurnOffHitCollider();
        }

        public void ActivateWeapon(bool activeMode)
        {
            gameObject.SetActive(activeMode);
            FlyWeapon?.SetActiveWeapon(!activeMode);
        }
    }
}