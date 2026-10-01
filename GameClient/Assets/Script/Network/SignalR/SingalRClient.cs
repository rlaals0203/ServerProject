using Core;
using Cysharp.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using UnityEngine;

public class SingalRClient : MonoSingleton<SingalRClient>
{
    private const string HUB_URL = "http://localhost:5000/gamehub";
    private HubConnection _connection;

    protected override void Awake()
    {
        base.Awake();
        _connection = new HubConnectionBuilder()
                    .WithUrl(HUB_URL)
                    .WithAutomaticReconnect()
                    .Build();
    }

    public void ConnectToHub<T1>(string methodName, Action<T1> handler)
    {
        _connection.On(methodName, handler);
    }

    public void ConnectToHub<T1, T2>(string methodName, Action<T1, T2> handler)
    {
        _connection.On(methodName, handler);
    }

    public void ConnectToHub<T1, T2, T3>(string methodName, Action<T1, T2, T3> handler)
    {
        _connection.On(methodName, handler);
    }

    public void ConnectToHub<T1, T2, T3, T4>(string methodName, Action<T1, T2, T3, T4> handler)
    {
        _connection.On(methodName, handler);
    }

    public async UniTask SendMessageToServer(string user, string message)
    {
        if (_connection != null && _connection.State == HubConnectionState.Connected)
        {
            try
            {
                await _connection.SendAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error sending message: {ex.Message}");
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(_connection != null)
        {
            _connection.DisposeAsync();
        }
    }
}