using System;

public class MakeRequestLoginPacket
{
    public UserLoginPacketData UserLoginPacketData;

    public MakeRequestLoginPacket(string userId, string password)
    {
        UserLoginPacketData userLoginPacket = new UserLoginPacketData
        {
            UserId = userId,
            Password = password
        };

        UserLoginPacketData = userLoginPacket;
    }

    private void HandleLoginResult(UserLoginPacketResultData data)
    {

    }
}