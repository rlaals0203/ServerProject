using Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public const string URL = "http://localhost:5200";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    private HttpClient _httpClient;

    protected override void Awake()
    {
        base.Awake();

        var cookieContainer = new CookieContainer();
        var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        _httpClient = new HttpClient(handler);

    }

    public async Task SendWebRequest<TRequest, TResult>(string route, TRequest request, Action<TResult> callback)
        where TRequest : StandardPacketData
        where TResult : StandardPacketResultData
    {
        if(GameManager.I.IsLoggin)
        {
            request.LoginUserId = GameManager.I.User.UserId;
        }

        string json = JsonSerializer.Serialize(request);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync($"{URL}/{route}", content);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            TResult resultPacket = JsonSerializer.Deserialize<TResult>(result, JsonOptions);
            callback?.Invoke(resultPacket);
            return;
        }

        Debug.Log($"POST {URL}/{route}\nRequest: {json}");
        Debug.LogError($"Response: {(int)response.StatusCode}\n{result}");
    }

    public async Task SendLoginRequest(string userId, string password, Action<UserLoginPacketResultData> callback)
        => await SendWebRequest(UserLoginPacketData.ROUTE, new MakeRequestLoginPacket(userId, password).UserLoginPacketData, callback);

    public async Task SendUnitUpgradeRequest(string unitId, int targetLevel, Action<UnitUpgradePacketResultData> callback)
        => await SendWebRequest(UnitUpgradePacketData.ROUTE, new MakeRequestUnitUpgradePacket(unitId, targetLevel).UnitUpgradePacketData, callback);

    public async Task SendUnitDeployRequest(Dictionary<int, string> deplotUnits, Action<UnitDeployPacketResultData> callback)
     => await SendWebRequest(UnitDeployPacketData.ROUTE, new MakeRequestUnitDeployPacket(deplotUnits).UnitDeployPacketData, callback);
}
