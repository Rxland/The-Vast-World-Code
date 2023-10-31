using System;
using _GAME.Code.Factories.Pools;
using _GAME.Code.Features;
using _GAME.Code.Logic;
using _GAME.Code.Types;
using UniRx;
using UnityEngine;
using Zenject;
using DeviceType = _GAME.Code.Types.DeviceType;

namespace _GAME.Code.Factories
{
    public class VfxFactory
    {
        [Inject] private DeadPuffEffectPool _deadPuffPool;
        [Inject] private BloodEffectPool _bloodEffectsPool;
        [Inject] private Sword_Slash_1_Pool _sword_Slash_1_Pool;
        [Inject] private Sword_Slash_3_Pool _sword_Slash_3_Pool;
        [Inject] private Sword_Slash_4_Pool _sword_Slash_4_Pool;
        [Inject] private Sword_Slash_8_Pool _sword_Slash_8_Pool;
        [Inject] private Prick_1_Pool _prick_1_Pool;
        [Inject] private DeviceFeature _deviceFeature;
        
        public void SpawnPooledEffect(EffectName effectName, Vector3 pos, Quaternion rotation, float destroyDelay = 2f)
        {
            if (_deviceFeature.CurrentDeviceType == DeviceType.Mobile && effectName != EffectName.DeadPuff)
                return;
            
            switch (effectName)
            {
                case EffectName.Blood:
                    SetUpEffect(_bloodEffectsPool.Spawn(), pos, rotation, destroyDelay, _bloodEffectsPool);
                    break;
                case EffectName.DeadPuff:
                    SetUpEffect( _deadPuffPool.Spawn(), pos, rotation, destroyDelay, _deadPuffPool);
                    break;
                case EffectName.SwordSlash_1:
                    SetUpEffect( _sword_Slash_1_Pool.Spawn(), pos, rotation, destroyDelay, _sword_Slash_1_Pool);
                    break;
                case EffectName.SwordSlash_3:
                    SetUpEffect( _sword_Slash_3_Pool.Spawn(), pos, rotation, destroyDelay, _sword_Slash_3_Pool);
                    break;
                case EffectName.SwordSlash_4:
                    SetUpEffect( _sword_Slash_4_Pool.Spawn(), pos, rotation, destroyDelay, _sword_Slash_4_Pool);
                    break;
                case EffectName.SwordSlash_8:
                    SetUpEffect( _sword_Slash_8_Pool.Spawn(), pos, rotation, destroyDelay, _sword_Slash_8_Pool);
                    break;
                case EffectName.Prick_1:
                    SetUpEffect( _prick_1_Pool.Spawn(), pos, rotation, destroyDelay, _prick_1_Pool);
                    break;
            }
        }
        
        private void SetUpEffect(Effect effect, Vector3 pos, Quaternion rotation, float destroyDelay = 2f, PoolBase<Effect> boolPool = null)
        {
            effect.gameObject.SetActive(true);
            effect.transform.position = pos;
            effect.transform.rotation = rotation;
            effect.Particle.Play();
            
            Observable.Timer(TimeSpan.FromSeconds(destroyDelay))
                .Subscribe(_ =>
                {
                    boolPool.Despawn(effect);
                    
                });
        }
    }
}