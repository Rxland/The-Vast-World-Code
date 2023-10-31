using System.Collections.Generic;
using _GAME.Code.Features;
using _GAME.Code.Types;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private PlayerRef _playerRef;
        [Space]
        
        [SerializeField] private List<Weapon.Weapon> AllWeapons;
        [ReadOnly] [SerializeField] private List<Weapon.Weapon> CurrentWeapons = new();
        [ReadOnly] [SerializeField] private Weapon.Weapon UsingWeapon;

        public int _currentAttackAnimId;

        [SerializeField] private float _animAttackSpeed;
        
        [Inject] private PlayerFeature _playerFeature;
        [Inject] private InventoryFeature _inventoryFeature;

        public void SetAttackSpeed(float speed)
        {
            _animAttackSpeed = speed;
        }
        
        public void Init()
        {
            CurrentWeapons.Clear();
            CurrentWeapons = _playerFeature.GetEquippedWeapons(AllWeapons);

            UsingWeapon = CurrentWeapons[0];
            
            _playerRef.AnimController.SetAnimInt(AnimName.Weapon_Id, UsingWeapon.weaponConfigData.AnimId);

            _animAttackSpeed = _inventoryFeature.GetAllEquippedStats(StatType.AttackSpeed);
            
            ActivateEquippedWeapons(false);
        }

        private void OnEnable()
        {
            _playerRef.PlayerInputController.OnAttackEvent += Attack;
        }
        private void OnDisable()
        {
            _playerRef.PlayerInputController.OnAttackEvent -= Attack;
        }

        private void ActivateEquippedWeapons(bool activeMode)
        {
            foreach (Weapon.Weapon currentWeapon in CurrentWeapons)
            {
                currentWeapon.ActivateWeapon(activeMode);
            }
        }
        
        public void ResetAttack()
        {
            _currentAttackAnimId = 0;
            
            ActivateEquippedWeapons(false);
            
            _playerRef.Animator.speed = 1;
            
            _playerRef.AnimController.SetAnimBool(AnimName.Attack, false);
            _playerRef.AnimController.SetAnimInt(AnimName.AttackStateId, 0);

            _playerRef.CharacterVariables.Attacking = false;

            DOVirtual.DelayedCall(0.3f, () => _playerRef.CharacterVariables.CanAttack = true);
        }
        
        private void Attack()
        {
            if (!_playerRef.CharacterVariables.CanAttack) return;
            
            _playerRef.CharacterVariables.CanAttack = false;
            _playerRef.CharacterVariables.Attacking = true;
            
            if (_currentAttackAnimId > UsingWeapon.MaxAnimStates)
                _currentAttackAnimId = 0;
            
            UsingWeapon.ActivateWeapon(true);
            
            _playerRef.Animator.speed = 1 + _animAttackSpeed;
            
            RotateByCameraView();
            
            _currentAttackAnimId++;
            
            _playerRef.AnimController.SetAnimBool(AnimName.Attack, true);
            _playerRef.AnimController.SetAnimInt(AnimName.AttackStateId, Mathf.Clamp(_currentAttackAnimId - 1, 0, UsingWeapon.MaxAnimStates));
        }

        private void RotateByCameraView()
        {
            transform.DORotate(_playerRef.CharacterVariables.CharacterCameraForward, _playerRef.CharacterVariables.AttackRotationSpeed);
        }
        
        #region For Anim Events

        public void StartAttack(int weaponId)
        {
            UsingWeapon = CurrentWeapons[weaponId];
            
            _playerRef.CharacterVariables.Attacking = true;
        }
        
        public void EndAttack(int attackId)
        {
            if (_currentAttackAnimId != attackId + 1)
            {
                return;
            }

            ResetAttack();
        }

        public void WaitToNextAttack()
        {
            _playerRef.CharacterVariables.CanAttack = true;
        }

        private void TurnOffCanAttack(AnimationEvent animationEvent)
        {
            _playerRef.CharacterVariables.CanAttack = false;
        }
        
        public void LastAttack()
        {
            ResetAttack();
        }

        public void PlayVfx(int id)
        {
            UsingWeapon.PlayVfx(id);
        }

        private void PlaySound(AnimationEvent animationEvent)
        {
            UsingWeapon.PlayRandomSlashSound();
        }
        
        public void Move(int id)
        {
            _playerRef.DashCharacterController.Dash(UsingWeapon.GetDashMoveData(id));
        }

        public void TurnOnWeaponHitCollider()
        {
            UsingWeapon.TurnOnWeaponHitCollider();
        }
        public void TurnOffWeaponHitCollider()
        {
            UsingWeapon.TurnOffWeaponHitCollider();
        }

        #endregion
        
    }
}