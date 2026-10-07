using UnityEngine;

namespace Scripts.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 1;
        [SerializeField] protected ContactFilter2D contactFier;

        protected Unit _owner;

        public virtual void InitCaster(Unit owner)
        {
            _owner = owner;
        }

        public abstract bool CastDamage(float damage, bool isCritical);
        public abstract bool CastDamage(Vector2 pos, float damage, bool isCritical);

        public Transform TargetTrm => _owner.transform;
    }
}