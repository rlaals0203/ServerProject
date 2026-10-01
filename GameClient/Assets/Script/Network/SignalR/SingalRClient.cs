using Core;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingalRClient : MonoSingleton<SingalRClient>
{
    private HubConnection _connection;

    protected override void Awake()
    {
        base.Awake();
        //_connection = new HubConnection();

    }
}
