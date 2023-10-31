using _GAME.Code.Features;
using _GAME.Code.Tools;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Interaction.Trigger
{
    public abstract class OnTriggerInteractionBase : MonoBehaviour
    {
        public bool CanInteract = true;

        [Inject] private StaticDataFeature _staticDataFeature;
        
        private void OnTriggerEnter(Collider collider)
        {
            if (!CanInteract || !GameExtensions.IsLayerInMask(_staticDataFeature.LayerMaskStaticData.PlayerLayerMask, collider.gameObject.layer)) return;
            
            Interact();
        }

        protected abstract void Interact();
    }
}