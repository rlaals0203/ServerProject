public class ComponentBase : IDisposable
{
    protected RuntimeUnit _owner;

    public ComponentBase(RuntimeUnit owner)
    {
        _owner = owner;
    }

    public void Dispose()
    {
        ReleaseComponent();
    }

    public virtual void InitializeComponent() { }
    public virtual void UpdateComponent(float dt) { }
    public virtual void ReleaseComponent() { }
}