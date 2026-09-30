using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Resource
{
    public class DefaultResourceContainer : ResourceContainer<Object>
    {
        public override async UniTask<Object> LoadAddressableResourcesAsync(string key)
        {
            var result = await LoadResourceAsyncInternal(key);
            return result;
        }
    }
}