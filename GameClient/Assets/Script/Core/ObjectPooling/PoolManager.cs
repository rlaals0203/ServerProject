using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private Transform root;

        private Dictionary<string, Pool> _poolList = new();

        public void RegisterPool(string key, GameObject go, int count)
        {
            if(string.IsNullOrEmpty(key))
            {
                return;
            }

            if(_poolList.ContainsKey(key))
            {
                return;
            }

            Pool newPool = new Pool();
            GameObject poolRoot = new GameObject($"{key}_Pool");
            poolRoot.transform.SetParent(root);

            _poolList[key] = newPool;

            for (int i = 0; i < count; i++)
            { 
                var spawned = Instantiate(go, root);
                spawned.SetActive(false);
                spawned.transform.SetParent(poolRoot.transform);

                PoolObject newPoolObject = new PoolObject
                {
                    Prefab = spawned
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

            if (_poolList.TryGetValue(key, out Pool pool) == false)
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

        public GameObject SpawnAndInitPool(string key, Vector3 position, Transform parent, Action spawnCallback = null)
        {
            GameObject spawned = SpawnPool(key);
            if(spawned == null)
            {
                return null;
            }

            spawned.transform.SetParent(parent);
            spawned.transform.position = position;
            spawnCallback?.Invoke();
            return spawned;
        }

        public void DespawnPool(string key, Action<int> onDespawn = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (_poolList.TryGetValue(key, out Pool pool) == false)
            {
                return;
            }

            pool.Pop();
            onDespawn?.Invoke(pool.CurrentPoolSize);
        }
    }
}