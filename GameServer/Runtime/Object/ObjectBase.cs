using System.Numerics;

public class ObjectBase
{
    protected Dictionary<Type, ComponentBase> _components = new();

    public RuntimeBattleField BattleField { get; set; }
    public Vector3 Position { get; set; }
    public Collider Collider { get; set; }
    public ELayer Layer { get; }

    public ObjectBase(RuntimeBattleField battleField, Vector3 position, ELayer layer)
    {
        BattleField = battleField;
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

    public virtual void Release() { }

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