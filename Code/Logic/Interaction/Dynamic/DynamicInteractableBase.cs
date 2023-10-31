using Unity.Collections;
using UnityEngine;

namespace _GAME.Code.Logic.Interaction.Dynamic
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class DynamicInteractableBase : MonoBehaviour
    {
        [ReadOnly] public BoxCollider Collider;
        
        public abstract void OnEnter();
        public abstract void OnExit();

        private void Reset()
        {
            Collider = GetComponent<BoxCollider>();
        }
    }
}