using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class MaterialResourceContainer : ResourceContainer<Material>
    {
        public override async UniTask<Material> LoadAddressableResourcesAsync(string key)
        {
            var result = await LoadResourceAsyncInternal(key);
            return result;
        }
    }
}