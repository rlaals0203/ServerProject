[Serializable]
public class UserLoginPacketData : StandardPacketData
{
    public const string ROUTE = "RequestUserLogin";
    public string UserId { get; set; }
    public string Password { get; set; }
}

[Serializable]
public class UserLoginPacketResultData : StandardPacketResultData
{
    public UserBaseData UserData { get; set; }
    public bool IsNewUser { get; set; }
    public UserLoginPacketResultData(EPacketResult result) : base(result) { }
}