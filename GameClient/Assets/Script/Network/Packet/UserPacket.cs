using System;

[Serializable]
public class UserLoginPacketData : StandardPacketData
{
    public const string ROUTE = "RequestUserLogin";
    public string UserId;
    public string Password;
}

[Serializable]
public class UserLoginPacketResultData : StandardPacketResultData
{
    public string UserId;
    public string Password;

    public UserLoginPacketResultData(EPacketResult result) : base(result) { }
}