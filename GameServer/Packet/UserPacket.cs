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

[Serializable]
public class UserMonetaChangePacketData : StandardPacketData
{
    public const string ROUTE = "RequestUserMonetaChange";
    public EMonetaType MonetaType { get; set; }
    public int Amount { get; set; }
}

[Serializable]
public class UserMonetaChangePacketResultData : StandardPacketResultData
{
    public EMonetaType MonetaType { get; set; }
    public int Amount { get; set; }

    public UserMonetaChangePacketResultData(EPacketResult packetResult) : base(packetResult)
    {
    }
}