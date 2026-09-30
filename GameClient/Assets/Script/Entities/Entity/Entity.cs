using System;
using System.Collections.Generic;
using UnityEngine;

public interface IComponentCache
{
    public void InitializeComponent(Entity owner);
}

public class Entity : MonoBehaviour
{
    protected Dictionary<Type, IComponentCache> _cachedComponent = new();

    private void Awake()
    {
        Initialize();
        InitializeComponentCaches();
    }

    protected virtual void Initialize() { }

    protected void InitializeComponentCaches()
    {
        var componentCaches = GetComponentsInChildren<IComponentCache>();
        foreach(var cache in componentCaches)
        {
            cache.InitializeComponent(this);
            _cachedComponent.Add(cache.GetType(), cache);
        }
    }

    public T Get<T>() where T : IComponentCache
    {
        return (T)_cachedComponent.GetValueOrDefault(typeof(T));
    }

    public bool TryGet<T>(out T compo) where T : IComponentCache
    {
        compo = default;
        if (_cachedComponent.TryGetValue(typeof(T), out var cache))
        {
            compo = (T)cache;
            return true;
        }

        return false;
    }
}
