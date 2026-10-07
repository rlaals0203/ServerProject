using System;
using System.Collections.Generic;
using System.Numerics;

public class RuntimeObjectBase
{
    Dictionary<Type, RuntimeComponent> _components;
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }

    public virtual void Init() { }
    public virtual void Update()
    {
        foreach(var component in _components.Values)
        {
            component.UpdateComponent();
        }
    }

    public T Get<T>() where T : RuntimeComponent
    {
        return (T)_components.GetValueOrDefault(typeof(T));
    }
}