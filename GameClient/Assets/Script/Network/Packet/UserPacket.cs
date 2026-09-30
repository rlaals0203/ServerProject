using System;

[Serializable]
public class UserLoginPacketData : PacketBase
{
    public string ROUTE = "RequestUserLogin";
    public string UserId { get; set; }
    public string Password { get; set; }
}

[Serializable]
public class UserLoginPacketDataResult : PacketResultBase
{
    public string UserId { get; set; }
    public string Password { get; set; }

    public UserLoginPacketDataResult(EPacketResult result) : base(result) { }
}