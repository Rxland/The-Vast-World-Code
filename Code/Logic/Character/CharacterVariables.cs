using UnityEngine;

namespace _GAME.Code.Logic.Character
{
    public class CharacterVariables : MonoBehaviour
    {
        [Header("Locomotion")]
        public Vector2 Move;
        public Vector2 Look;
        
        public bool Jump;
        public bool Sprint;
        public bool IsMoving;

        [Header("Attack")] 
        public bool CanAttack;
        public bool Attacking;
        public float AttackRotationSpeed;

        [Header("Camera")] 
        public Vector3 CharacterCameraForward;
    }
}