using _GAME.Code.Factories;
using _GAME.Code.Logic.Player;
using _GAME.Code.Types;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Character.Stats
{
    public class PlayerStats : StatsBase
    {
        private PlayerRef _playerRef;

        [Inject] private WindowFactory _windowFactory;

        public override void Init()
        {
            MaxHp += Mathf.RoundToInt(InventoryFeature.GetAllEquippedStats(StatType.Hp));
            _playerRef = GetComponent<PlayerRef>();
            base.Init();
        }

        protected override void Hit(int damage, Transform damageFromT)
        {
            base.Hit(damage, damageFromT);
            
            _playerRef.AnimController.SetAnimTrigger(AnimName.Stunned);
        }

        public override void Kill()
        {
            base.Kill();

            _playerRef.PlayerAttackController.ResetAttack();
            
            _playerRef.PlayerAttackController.enabled = false;
            _playerRef.ThirdPersonController.enabled = false;
            
            
            _playerRef.AnimController.SetAnimBool(AnimName.Idle, false);
            _playerRef.AnimController.SetAnimTrigger(AnimName.Die);

            DOVirtual.DelayedCall(2f, () => _windowFactory.SpawnWindow(WindowName.Revive));
        }

        public override void Revive()
        {
            base.Revive();
            
            _playerRef.AnimController.SetAnimBool(AnimName.Idle, true);
            
            _playerRef.PlayerAttackController.enabled = true;
            _playerRef.ThirdPersonController.enabled = true;
        }
    }
}