using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private Transform root;

        private Dictionary<string, Pool> _poolDict;

        public void RegisterPool(string key, GameObject go, int count)
        {
            if(string.IsNullOrEmpty(key))
            {
                return;
            }

            Pool newPool = new Pool();
            _poolDict[key] = newPool;

            for (int i = 0; i < count; i++)
            {
                var spawned = Instantiate(go, root);
                spawned.SetActive(false);

                PoolObject newPoolObject = new PoolObject
                {
                    Prefab = go
                };

                newPool.Push(newPoolObject);
            }
        }
        
        public GameObject SpawnPool(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (_poolDict.TryGetValue(key, out Pool pool) == false)
            {
                return null;
            }

            if(pool.CurrentPoolSize >= pool.MaxPoolSize)
            {
                return null;
            }

            var spawned = pool.Pop();
            if(spawned == null)
            {
                Debug.LogError($"spawned pool is null");
                return null;
            }

            return spawned.Prefab;
        }

        public void SpawnAndInitPool(string key, Vector3 position, Transform parent, Action spawnCallback = null)
        {
            GameObject spawned = SpawnPool(key);
            if(spawned == null)
            {
                return;
            }

            spawned.transform.SetParent(parent);
            spawned.transform.position = position;
            spawnCallback?.Invoke();
        }

        public void DespawnPool(string key, Action<int> onDespawn = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (_poolDict.TryGetValue(key, out Pool pool) == false)
            {
                return;
            }

            pool.Pop();
            onDespawn?.Invoke(pool.CurrentPoolSize);
        }
    }
}