using _GAME.Code.Logic.Player;
using _GAME.Code.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Logic.Character
{
    public class CharacterAttackController : MonoBehaviour
    {
        [ReadOnly] public bool IsDetectedAttackTarget;
        [Space] 
        
        [SerializeField] private AnimController _AnimController;
        public Weapon.Weapon Weapon;
        public float Damage;
        public float AttackDetectionRange;
        public CharacterAttackType AttackType;
        public LayerMask AttackLayerMask;

        private float _currentAttackAnimId; 
        
        public void ResetAttack()
        {
            _currentAttackAnimId = 0;
            
            _AnimController.SetAnimBool(AnimName.Attack, false);
            _AnimController.SetAnimInt(AnimName.AttackStateId, 0);
        }
        
        
        #region For Anim Events

        public void Shoot()
        {
            Weapon.Shoot();
        }
        
        public void EndAttack(int attackId)
        {
            if (_currentAttackAnimId != attackId + 1)
            {
                return;
            }

            ResetAttack();
        }
        
        public void LastAttack()
        {
            ResetAttack();
        }

        public void PlayVfx(int id)
        {
            Weapon.PlayVfx(id);
        }

        public void Move(int id)
        {
            // _playerRef.DashCharacterController.Dash(Weapon.GetDashMoveData(id));
        }

        public void TurnOnWeaponHitCollider()
        {
            Weapon.TurnOnWeaponHitCollider();
        }
        public void TurnOffWeaponHitCollider()
        {
            Weapon.TurnOffWeaponHitCollider();
        }

        #endregion
    }
}