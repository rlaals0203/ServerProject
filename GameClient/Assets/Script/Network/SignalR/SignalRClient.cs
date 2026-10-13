using Core;
using Cysharp.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using UnityEngine;

public class SignalRClient : MonoSingleton<SignalRClient>
{
    private const string HUB_URL = "http://localhost:5200/gamehub";
    private HubConnection _connection;

    protected override void Awake()
    {
        base.Awake();
        _connection = new HubConnectionBuilder()
                    .WithUrl(HUB_URL)
                    .WithAutomaticReconnect()
                    .Build();
    }

    private void Start()
    {
        _connection.StartAsync();
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

    public async UniTask SendMessageToServer(string method, string userId)
    {
        if (_connection != null && _connection.State == HubConnectionState.Connected)
        {
            try
            {
                Debug.Log($"서버로 전송 {method}, {userId}");
                await _connection.SendAsync(method, userId);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error sending message: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("SignalR not connected.");
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