using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public async void OnTouch()
    {
        var testPacket = new MakeRequestLoginPacket(string.Empty, "1234").UserLoginPacketData;
        await NetworkManager.I.SendWebRequest<UserLoginPacketData, UserLoginPacketResultData>(testPacket.ROUTE, testPacket, HandleLoginResult);
    }

    private void HandleLoginResult(UserLoginPacketResultData data)
    {
    }
}
