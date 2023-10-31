using System;
using UnityEngine;

namespace _GAME.Code.Logic.Extentions
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityMultiplier : MonoBehaviour
    {
        [SerializeField] public bool IsOn = true;
        [Space]
        
        [SerializeField] private float _gravityMultiplier = 2.0f;
        [SerializeField] private Rigidbody _rigidbody;
        
        
        private void FixedUpdate()
        {
            if (!IsOn) return;
            
            Vector3 customGravity = _gravityMultiplier * Physics.gravity;
            _rigidbody.AddForce(customGravity, ForceMode.Acceleration);
        }

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}