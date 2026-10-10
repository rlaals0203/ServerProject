using Core;
using Cysharp.Threading.Tasks;
using ObjectPooling;
using Resource;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MINLib.Script.Core.Resource
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        private DefaultResourceContainer _defaultResourceContainer;
        private PrefabResourceContainer _prefabResourceContainer;
        private MaterialResourceContainer _materialResourceContainer;
        private SpriteResourceContainer _spriteResourceContainer;

        private Dictionary<string, UnityEngine.Object> _resourcePool = new();

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _defaultResourceContainer = new DefaultResourceContainer();
            _prefabResourceContainer = new PrefabResourceContainer();
            _materialResourceContainer = new MaterialResourceContainer();
            _spriteResourceContainer = new SpriteResourceContainer();
            PreSpawnPool();
        }

        private async UniTask PreSpawnPool()
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync("prepooling", typeof(GameObject));
            await locationsHandle.Task;

            if (locationsHandle.Status != AsyncOperationStatus.Succeeded)
                return;

            foreach (var location in locationsHandle.Result)
            {
                int spawnCount = 1;
                string key = location.PrimaryKey;
                var prefab = await _prefabResourceContainer.LoadAddressableResourcesAsync(key);

                if (prefab == null)
                {
                    Debug.LogError($"prefab of {key} is null");
                    continue;
                }
                
                if (prefab.TryGetComponent(out PoolSetting poolSetting))
                {
                    spawnCount = poolSetting.GetPrewarmCount();
                }

                _resourcePool[key] = prefab;
                PoolManager.I.RegisterPool(key, prefab, spawnCount);
            }

            Addressables.Release(locationsHandle);
        }    

        public async UniTask<T> LoadAddressableResourceAsync<T>(string key) where T : UnityEngine.Object
        {
            if(string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (TryGetCachedResource(key, out T cachedResource))
            {
                return cachedResource;
            }

            ResourceContainer<T> resourceContainer = GetResourceContainer<T>();
            var result = await resourceContainer.LoadAddressableResourcesAsync(key);
            return result;
        }

        public bool TryGetResource<T>(string key, out T resource) where T : UnityEngine.Object
        {
            ResourceContainer<T> resourceContainer = GetResourceContainer<T>();
            return resourceContainer.TryGetResource(key, out resource);
        }

        public async UniTask CachingResource(string key, Action<string> onComplete)
        {
            if(await CachingResource(key))
            {
                onComplete?.Invoke(key);
            }
        }

        public async UniTask<bool> CachingResource(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return false;
            }

            if(CheckCacheResource(key))
            {
                return true;
            }

            GameObject loadedObject = await LoadAddressableResourceAsync<GameObject>(key);

            if (loadedObject == null)
            {
                return false;
            }

            return true;
        }

        public GameObject SpawnAndInitPool(string key, Vector3 position, Transform parent, Action spawnCallback = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError($"pool key is empty: {key}");
                return null;
            }

            if (_prefabResourceContainer.CheckResourceLoaded(key) == false)
            {
                Debug.LogError($"can't loaded cache: {key}");
                return null;
            }

            var spawned = PoolManager.I.SpawnAndInitPool(key, position, parent, spawnCallback);

            if(spawned == null)
            {
                Debug.LogError($"spawned pool is null: {key}");
                return null;
            }

            return spawned;
        }

        public T SpawnAndInitPool<T>(string key, Vector3 position, Transform parent, Action spawnCallback = null)
        {
            if(CheckCacheResource(key) == false)
            {
                return default;
            }

            GameObject spawned = SpawnAndInitPool(key, position, parent, spawnCallback);

            if(spawned == null)
                return default;

            return spawned.GetComponent<T>();
        }

        public bool DespawnPool(GameObject target, Action<int> onDespawn = null)
        {
            return PoolManager.I.DespawnPool(target, onDespawn);
        }

        public bool CheckCacheResource(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (_resourcePool.TryGetValue(key, out UnityEngine.Object cachedResource) == false)
            {
                return false;
            }

            if (cachedResource != null)
            {
                return true;
            }

            _resourcePool.Remove(key);
            return false;
        }

        private bool TryGetCachedResource<T>(string key, out T resource) where T : UnityEngine.Object
        {
            resource = null;
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (_resourcePool.TryGetValue(key, out UnityEngine.Object cachedResource) == false)
            {
                return false;
            }

            if (cachedResource == null)
            {
                _resourcePool.Remove(key);
                return false;
            }

            resource = cachedResource as T;
            if (resource != null)
            {
                return true;
            }

            return false;
        }
        public ResourceContainer<T> GetResourceContainer<T>() where T : UnityEngine.Object
        {
            Type type = typeof(T);
            if (type == typeof(GameObject))
                return _prefabResourceContainer as ResourceContainer<T>;
            if (type == typeof(Material))
                return _materialResourceContainer as ResourceContainer<T>;
            if (type == typeof(Sprite))
                return _spriteResourceContainer as ResourceContainer<T>;

            return _defaultResourceContainer as ResourceContainer<T>;
        }
    }
}
