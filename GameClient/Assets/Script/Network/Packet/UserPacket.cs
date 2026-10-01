using System;

[Serializable]
public class UserLoginPacketData : PacketBase
{
    public string ROUTE = "RequestUserLogin";
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