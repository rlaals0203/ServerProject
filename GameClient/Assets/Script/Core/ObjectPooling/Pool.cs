using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [System.Serializable]
    public class PoolObject
    {
        public GameObject Prefab;
    }
    
    public class Pool
    {
        [HideInInspector] public int MaxPoolSize = 10;
        
        private Queue<PoolObject> _pool;
        public int CurrentPoolSize => _pool.Count;

        public Pool()
        {
            _pool = new Queue<PoolObject>();
        }
        
        public PoolObject Pop()
        {
            if (_pool.Count == 0)
            {
                Debug.LogWarning("Pool is empty");
                return null;
            }
            
            var result = _pool.Dequeue();
            return result;
        }

        public void Push(PoolObject poolObject)
        {
            if (CurrentPoolSize >= MaxPoolSize)
                return;
            
            _pool.Enqueue(poolObject);
        }
    }
}