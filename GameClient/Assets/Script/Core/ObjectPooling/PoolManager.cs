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
        private Dictionary<GameObject, Pool> _pooledObjects = new();

        public void RegisterPool(string key, GameObject go, int count)
        {
            if (count <= 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if(_poolList.ContainsKey(key))
            {
                return;
            }

            GameObject poolRoot = new GameObject($"{key}_Pool");
            poolRoot.transform.SetParent(root);
            Pool newPool = new Pool(poolRoot.transform);

            _poolList[key] = newPool;

            for (int i = 0; i < count; i++)
            { 
                var spawned = Instantiate(go, poolRoot.transform);
                spawned.SetActive(false);
                newPool.Push(spawned);
                _pooledObjects.Add(spawned, newPool);
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

            GameObject spawned = pool.Pop();
            if(spawned == null)
            {
                return null;
            }

            return spawned;
        }

        public GameObject SpawnAndInitPool(string key, Vector3 position, Transform parent, Action spawnCallback = null)
        {
            GameObject spawned = SpawnPool(key);
            if(spawned == null)
            {
                return null;
            }

            spawned.gameObject.SetActive(true);
            spawned.transform.SetParent(parent, false);
            spawned.transform.position = position;
            spawnCallback?.Invoke();
            return spawned;
        }

        public bool DespawnPool(GameObject target, Action<int> onDespawn = null)
        {
            if (target == null)
            {
                return false;
            }

            if (_pooledObjects.TryGetValue(target, out Pool pool) == false)
            {
                return false;
            }

            if (pool.Push(target) == false)
            {
                return false;
            }

            target.SetActive(false);
            target.transform.SetParent(pool.Root, false);

            onDespawn?.Invoke(pool.CurrentPoolSize);
            return true;
        }
    }
}
