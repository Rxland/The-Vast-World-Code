using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Factories.Pools
{
    public class PoolBase<T> : MemoryPool<T> where T : Component
    {
        public List<T> ActiveObjects => activeObjects;
        
        private List<T> activeObjects = new List<T>();

        protected override void OnCreated(T item)
        {
            base.OnCreated(item);
            item.gameObject.SetActive(false);
        }

        protected override void Reinitialize(T item)
        {
            base.Reinitialize(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnDespawned(T item)
        {
            base.OnDespawned(item);
            item.gameObject.SetActive(false);
            activeObjects.Remove(item);
        }

        protected override void OnSpawned(T item)
        {
            base.OnSpawned(item);
            item.gameObject.SetActive(true);
            activeObjects.Add(item);
        }

        public void DespawnAll()
        {
            foreach (var item in activeObjects)
            {
                Despawn(item);
            }

            activeObjects.Clear();
        }
    }
}