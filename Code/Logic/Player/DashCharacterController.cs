using System;
using _GAME.Code.Features;
using TrailsFX;
using UniRx;
using UnityEngine;
using Zenject;
using DeviceType = _GAME.Code.Types.DeviceType;

namespace _GAME.Code.Logic.Player
{
    public class DashCharacterController : MonoBehaviour
    {
        [SerializeField] private PlayerRef _playerRef;
        [Space] 
        
        [SerializeField] public DashMoveData DefaultDashMoveData;
        [SerializeField] private float cooldown = 2f;
        
        private float FireTime = 0f;
        private bool _isOnCooldown;


        [Inject] private DeviceFeature _deviceFeature;
        
        public void DashWithCooldown(Action onDashEvent, DashMoveData dashMoveData, Action onDashDone)
        {
            if (_isOnCooldown) return;
            
            _playerRef.PlayerAttackController.ResetAttack();
            
            if (_playerRef.CharacterVariables.Attacking) return;

            _isOnCooldown = true;
            
            onDashEvent?.Invoke();
            
            EnableTrailEffects();
            
            Dash(dashMoveData, onDashDone);
            
            Observable.Timer(TimeSpan.FromSeconds(cooldown))
                .Subscribe(_ =>
                {
                    _isOnCooldown = false;
                })
                .AddTo(this);
        }
        
        public void  Dash(DashMoveData dashMoveData, Action onDashDone = null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(dashMoveData.Range / dashMoveData.Speed);
            
            Observable.EveryUpdate()
                .TakeUntil(Observable.Timer(timeSpan))
                .Subscribe(_ =>
                {
                    _playerRef.CharacterController.Move(transform.forward * dashMoveData.Speed * Time.deltaTime);
                })
                .AddTo(this);

            Observable.Timer(timeSpan)
                .Subscribe(_ =>
                {
                    onDashDone?.Invoke();
                })
                .AddTo(this);
        }

        private void EnableTrailEffects()
        {
            if (_deviceFeature.CurrentDeviceType == DeviceType.Mobile) return;
            
            foreach (TrailEffect trailEffect in _playerRef.TrailEffects)
            {
                trailEffect.active = true;
            }
        }
    }

    [Serializable]
    public struct DashMoveData
    {
        public float Range;
        public float Speed;
    }
}