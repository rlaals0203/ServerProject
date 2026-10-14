using Core;
using Cysharp.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using UnityEngine;

public class SignalRClient : MonoSingleton<SignalRClient>
{
    private const string HUB_URL = "http://localhost:5200/gamehub";
    private HubConnection _connection;
    private readonly SemaphoreSlim _lock = new(1, 1);

    protected override void Awake()
    {
        base.Awake();
        _connection = new HubConnectionBuilder()
                    .WithUrl(HUB_URL)
                    .WithAutomaticReconnect()
                    .Build();
    }

    private async void Start()
    {
        try
        {
            await ConnectAsync();
        }
        catch(Exception ex)
        {
            Debug.LogError($"SignalR connection failed: {ex.Message}");
        }
    }

    private async UniTask ConnectAsync()
    {
        await _lock.WaitAsync();
        try
        {
            if(_connection.State == HubConnectionState.Disconnected)
                await _connection.StartAsync();
        }
        finally
        {
            _lock.Release();
        }
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

    public IDisposable SubscribeToHub<T1>(string methodName, Action<T1> handler)
    {
        return _connection.On(methodName, handler);
    }

    public async UniTask SendMessageToServer(string method, string userId)
    {
        try
        {
            await ConnectAsync();
            if(_connection.State != HubConnectionState.Connected)
            {
                Debug.LogWarning("SignalR not connected.");
                return;
            }

            Debug.Log($"서버로 전송 {method}, {userId}");
            await _connection.SendAsync(method, userId);
        }
        catch(Exception ex)
        {
            Debug.LogError($"Error sending message: {ex.Message}");
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