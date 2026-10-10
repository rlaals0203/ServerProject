using System.Numerics;

public class ObjectBase
{
    protected Dictionary<Type, ComponentBase> _components = new();

    public Vector3 Position { get; set; }
    public Collider Collider { get; set; }
    public ELayer Layer { get; }

    public ObjectBase(Vector3 position, ELayer layer)
    {
        Position = position;
        Layer = layer;
        
        Collider = new Collider();
    }

    public virtual void Init() { }
    public virtual void Update(float dt)
    {
        foreach(var component in _components.Values)
        {
            component.UpdateComponent(dt);
        }
    }

    public void AddComponent<T>(T component) where T : ComponentBase
    {
        _components.Add(component.GetType(), component);
    }

    public T Get<T>() where T : ComponentBase
    {
        return (T)_components.GetValueOrDefault(typeof(T));
    }

    public bool TryGetComponent<T>(out T component) where T : ComponentBase
    {
        component = null;
        
        if(_components.TryGetValue(typeof(T), out var compo))
        {
            component = (T)compo;
            return true;
        }

        return false;
    }
}