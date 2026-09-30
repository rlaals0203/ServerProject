using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class SpriteResourceContainer : ResourceContainer<Sprite>
    {
        public override async UniTask<Sprite> LoadAddressableResourcesAsync(string key)
        {
            var result = await LoadResourceAsyncInternal(key);
            return result;
        }
    }
}