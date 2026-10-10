using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pool
    {
        private Queue<GameObject> _pool = new();

        public Transform Root { get; }
        public int CurrentPoolSize => _pool.Count;

        public Pool(Transform parent)
        {
            Root = parent;
        }
        
        public GameObject Pop()
        {
            if (_pool.Count == 0)
            {
                Debug.LogWarning("Pool is empty");
                return null;
            }
            
            var result = _pool.Dequeue();
            return result;
        }

        public bool Push(GameObject poolObject)
        {
            if (poolObject == null || _pool.Contains(poolObject))
                return false;
            
            _pool.Enqueue(poolObject);
            return true;
        }
    }
}
