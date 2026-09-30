using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core
{
    public class ResourceHandle<T> where T : UnityEngine.Object
    {
        public AsyncOperationHandle<T> Handle;
        public T Resource;
        public int RefCount;
    }
    public abstract class ResourceContainer<T> where T : UnityEngine.Object
    {
        private readonly Dictionary<string, ResourceHandle<T>> _handles = new();

        public abstract UniTask<T> LoadAddressableResourcesAsync(string key);

        protected async UniTask<T> LoadResourceAsyncInternal(string key)
        {
            if (_handles.TryGetValue(key, out ResourceHandle<T> handle) == false)
            {
                AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(key);
                handle = new ResourceHandle<T>()
                {
                    Handle = loadHandle,
                    Resource = loadHandle.Result,
                    RefCount = 0
                };

                _handles.Add(key, handle);
            }

            handle.RefCount++;

            try
            {
                await handle.Handle.Task;

                if (handle.Handle.Status != AsyncOperationStatus.Succeeded)
                {
                    throw new System.InvalidOperationException($"resource load failed: {key}");
                }

                if (handle.Handle.Result == null)
                {
                    throw new System.InvalidOperationException($"resource object load failed: {key}");
                }

                handle.Resource = handle.Handle.Result;
                return handle.Resource;
            }
            catch
            {
                if (_handles.TryGetValue(key, out ResourceHandle<T> currentHandle) && ReferenceEquals(currentHandle, handle))
                    ReleaseResource(key);

                throw;
            }
        }

        public bool TryGetResource(string key, out T resource)
        {
            resource = null;
            if (_handles.TryGetValue(key, out ResourceHandle<T> handle))
            {
                resource = handle.Resource;
            }

            if (resource == null)
            {
                Debug.LogWarning($"resource not found: {key}");
                return false;
            }

            return resource;
        }

        public bool CheckResourceLoaded(string key)
        {
            var handle = GetResourceHandleInternal(key);

            if (handle == null)
            {
                Debug.LogWarning($"resource handle {key} is null");
                return false;
            }

            return handle.Handle.IsValid();
        }

        public void ReleaseResource(string key)
        {
            var handle = GetResourceHandleInternal(key);

            if (handle == null)
            {
                Debug.LogWarning($"resource handle {key} is null");
                return;
            }

            handle.RefCount--;

            if (handle.RefCount <= 0)
            {
                Addressables.Release(handle.Handle);
                _handles.Remove(key);
            }
        }

        private ResourceHandle<T> GetResourceHandleInternal(string key)
        {
            if (_handles.TryGetValue(key, out ResourceHandle<T> handle) == false)
            {
                Debug.LogWarning($"target resource not found: {key}");
            }

            return handle;
        }
    }
}