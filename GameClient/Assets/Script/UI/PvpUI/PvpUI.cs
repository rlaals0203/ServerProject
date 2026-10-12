using System;
using System.Collections;
using UnityEngine;

public class PvpUI : ContentUI
{
    protected override void Awake()
    {
        base.Awake();

        SignalRClient.I.ConnectToHub<MatchResultData>("JoinMatchResult", OnRecieveResult);
    }
    public override async void Initialize()
    {
        await SignalRClient.I.SendMessageToServer("JoinMatch", GameManager.I.MainUser.UserId);
    }

    private void OnRecieveResult(MatchResultData data)
    {
        Debug.Log(data.MatchResult);
    }
}