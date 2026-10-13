using MINLib.Script.Core.Resource;
using UnityEngine;

public abstract class EffectBase : MonoBehaviour
{
    protected bool _isPlaying;
    protected float _leftDuration;

    public virtual void PlayEffect(Vector3 position, Quaternion rotation, float duration)
    {
        _isPlaying = true;
        _leftDuration = duration;

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
    }

    public abstract void StopEffect();

    protected virtual void Update()
    {
        _leftDuration -= Time.deltaTime;

        if(_leftDuration <= 0f && _isPlaying)
        {
            _isPlaying = false;
            StopEffect();
            ReleaseEffect();
        }
    }

    public virtual void ReleaseEffect()
    {
        ResourceManager.I.DespawnPool(gameObject);
    }
}