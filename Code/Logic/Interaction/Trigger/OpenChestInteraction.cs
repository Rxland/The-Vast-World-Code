using UnityEngine;

namespace _GAME.Code.Logic.Interaction.Trigger
{
    public class OpenChestInteraction : OnTriggerInteractionBase
    {
        [SerializeField] private Chest _chest;
        
        protected override void Interact()
        {
            if (!CanInteract) return;
            
            CanInteract = false;
            
            _chest.OpenChest();
        }
    }
}