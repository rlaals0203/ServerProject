using UnityEngine;

namespace Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T Instance;
        private static readonly object _lock = new();

        public static bool HasInstance => Instance != null;
        public virtual int ReleaseOrder => 0; //재정의로 0이 아니면 별도로 Release 동작

        public static T I
        {
            get
            {
                if (Instance != null)
                {
                    return Instance;
                }

                lock (_lock)
                {
                    if (Instance != null)
                        return Instance;

                    Instance = FindAnyObjectByType<T>();

                    if (Instance == null)
                    {
                        Debug.LogError($"[MonoSingleton] No instance of {typeof(T)} found in scene.");
                    }
                }

                return Instance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Debug.LogWarning($"[MonoSingleton] Duplicate instance of {typeof(T)} detected. Destroying {gameObject.name}.");
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (ReleaseOrder == 0 && Instance == this)
            {
                Instance = null;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
        }
    }
}