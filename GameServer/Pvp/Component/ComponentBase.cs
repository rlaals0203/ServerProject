public class ComponentBase
{
    protected PvpUnit Owner;

    public ComponentBase(PvpUnit owner)
    {
        Owner = owner;
    }

    public virtual void InitializeComponent() { }
    public virtual void UpdateComponent() { }
}