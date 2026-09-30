using UnityEngine;

namespace Core
{
    public class GameObjectBase
    {
        public Transform transform => transformCache;
        private Transform transformCache;
    }
}