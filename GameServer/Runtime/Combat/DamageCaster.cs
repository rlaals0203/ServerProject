public class DamageCaster
{
    private RuntimeUnit _owner;

    public DamageCaster(RuntimeUnit owner)
    {
        _owner = owner;
    }

    public void CastDamage()
    {

    }

    public bool DetectTargets()
    {
        return true;
    }
}