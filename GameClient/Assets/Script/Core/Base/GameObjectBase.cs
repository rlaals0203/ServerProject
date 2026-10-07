using UnityEngine;

namespace Core
{
    public static class GameObjectExtensions
    {
        public static void SetLayer(this GameObject gameObject, int layer, bool includeChildren = false)
        {
            if (gameObject == null) return;

            if (!includeChildren)
            {
                gameObject.layer = layer;
                return;
            }

            Transform[] family = gameObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < family.Length; i++)
            {
                family[i].gameObject.layer = layer;
            }
        }

        public static void SetLayer(this GameObject gameObject, string layerName, bool includeChildren = false)
        {
            int layer = LayerMask.NameToLayer(layerName);

            if (layer == -1)
            {
                return;
            }

            gameObject.SetLayer(layer, includeChildren);
        }

        public static void SetLayer(this GameObject gameObject, LayerMask layerMask, bool includeChildren = false)
        {
            int layerIndex = 0;
            int maskValue = layerMask.value;

            if (maskValue == 0) return;

            while ((maskValue & 1) == 0)
            {
                maskValue >>= 1;
                layerIndex++;
            }

            gameObject.SetLayer(layerIndex, includeChildren);
        }
    }

}