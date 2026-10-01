[Serializable]
public class UserLoginPacketData : PacketDataBase
{
    public const string ROUTE = "RequestUserLogin";
    public string UserId;
    public string Password;
}

[Serializable]
public class UserLoginPacketResultData : PacketResultDataBase
{
    public string UserId;
    public string Password;

    public UserLoginPacketResultData(EPacketResult result) : base(result) { }
}