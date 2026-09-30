using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class PrefabResourceContainer : ResourceContainer<GameObject>
    {
        public override async UniTask<GameObject> LoadAddressableResourcesAsync(string key)
        {
            var result = await LoadResourceAsyncInternal(key);
            return result;
        }
    }
}