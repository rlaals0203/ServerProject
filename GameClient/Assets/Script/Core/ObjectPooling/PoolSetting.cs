using UnityEngine;

namespace ObjectPooling
{
    public class PoolSetting : MonoBehaviour
    {
        [SerializeField] private int createCount = 1;

        public int GetPrewarmCount() => createCount;
    }
}
