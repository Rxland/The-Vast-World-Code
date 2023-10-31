using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Logic.Player
{
    public class AnimController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetAnimBool(AnimName animName, bool mode)
        {
            _animator.SetBool(animName.ToString(), mode);
        }
        public void SetAnimTrigger(AnimName animName)
        {
            _animator.SetTrigger(animName.ToString());
        }
        public void SetAnimInt(AnimName animName, int value)
        {
            _animator.SetInteger(animName.ToString(), value);
        }
    }
}