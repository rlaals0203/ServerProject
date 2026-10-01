[Serializable]
public class UserLoginPacketData : PacketBase
{
    public const string ROUTE = "RequestUserLogin";
    public string UserId;
    public string Password;
}

[Serializable]
public class UserLoginPacketResultData : PacketResultBase
{
    public string UserId;
    public string Password;

    public UserLoginPacketResultData(EPacketResult result) : base(result) { }
}