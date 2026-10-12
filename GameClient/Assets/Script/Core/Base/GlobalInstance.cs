using Core;

public class GlobalInstance : MonoSingleton<GlobalInstance>
{
    public bool IsLoggin = false;
    public UserBaseData MainUser = null;
    public MapDataSO CurrentMap { get; set; }
}
