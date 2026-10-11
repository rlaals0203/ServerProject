using Core;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();


    }

    public bool IsLoggin = false;
    public UserBaseData MainUser = null;
    public MapDataSO CurrentMap { get; set; }
}
