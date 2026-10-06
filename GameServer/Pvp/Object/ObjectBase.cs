using System.Numerics;

public class ObjectBase
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public Collider Collider { get; set; }

    public virtual void Init() { }
    public virtual void Update(float dt) { }
}