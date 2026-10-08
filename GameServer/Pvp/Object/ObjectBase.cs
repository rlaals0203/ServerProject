using System.Numerics;

public class ObjectBase
{
    protected Dictionary<Type, ComponentBase> _components;

    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public Collider Collider { get; set; }

    public virtual void Init() { }
    public virtual void Update(float dt)
    {
        foreach(var component in _components.Values)
        {
            component.UpdateComponent();
        }
    }

    public T Get<T>() where T : ComponentBase
    {
        return (T)_components.GetValueOrDefault(typeof(T));
    }

    public void AddComponent<T>(T component) where T : ComponentBase
    {
        _components.Add(component.GetType(), component);
    }
}