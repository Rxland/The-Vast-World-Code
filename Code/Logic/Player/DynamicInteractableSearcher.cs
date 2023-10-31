using System;
using System.Collections.Generic;
using System.Linq;
using _GAME.Code.Logic.Interaction.Dynamic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Logic.Player
{
    public class DynamicInteractableSearcher : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _range;

        [SerializeField] private float _searchInterval = 1f;
        private float _timeSinceLastSearch = 0f;
        
        [Header("Editor")] 
        [SerializeField] private Color _rangeColor;

        
        [ReadOnly] [ShowInInspector] private List<DynamicInteractableBase> _entererInteractables = new();
        [ReadOnly] [ShowInInspector] private List<Collider> _enteredInteractablesColliders = new();

        private void Update()
        {
            _timeSinceLastSearch += Time.deltaTime;

            if (_timeSinceLastSearch >= _searchInterval)
            {
                SearchInteractions();
                _timeSinceLastSearch = 0f;
            }
        }

        private void SearchInteractions()
        {
            List<Collider> colliders = Physics.OverlapSphere(transform.position, _range, _layerMask).ToList();
            List<Collider> collidersToRemove = new List<Collider>();

            foreach (Collider collider in _enteredInteractablesColliders)
            {
                if (!colliders.Contains(collider))
                {
                    collidersToRemove.Add(collider);

                    // Find the associated interactable
                    if (collider.TryGetComponent(out DynamicInteractableBase interactable))
                    {
                        interactable.OnExit();
                    }
                }
            }

            foreach (Collider colliderToRemove in collidersToRemove)
            {
                _enteredInteractablesColliders.Remove(colliderToRemove);

                if (colliderToRemove.TryGetComponent(out DynamicInteractableBase interactableToRemove))
                {
                    _entererInteractables.Remove(interactableToRemove);
                }
            }

            foreach (Collider collider in colliders)
            {
                if (_enteredInteractablesColliders.Contains(collider)) continue;

                if (collider.TryGetComponent(out DynamicInteractableBase interactable))
                {
                    interactable.OnEnter();
                    _enteredInteractablesColliders.Add(collider);
                    _entererInteractables.Add(interactable);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _rangeColor;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}