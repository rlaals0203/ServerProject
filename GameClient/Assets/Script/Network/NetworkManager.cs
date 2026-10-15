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
        if(GlobalInstance.I.IsLoggin)
        {
            request.LoginUserId = GlobalInstance.I.MainUser.UserId;
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

    public async Task SendUnitUnlockRequest(string unitId, Action<UnitUnlockPacketResultData> callback)
        => await SendWebRequest(UnitUnlockPacketData.ROUTE, new MakeRequestUnitUnlockPacket(unitId).UnitUnlockPacket, callback);

    public async Task SendUnitUpgradeRequest(string unitId, int targetLevel, Action<UnitUpgradePacketResultData> callback)
        => await SendWebRequest(UnitUpgradePacketData.ROUTE, new MakeRequestUnitUpgradePacket(unitId, targetLevel).UnitUpgradePacketData, callback);

    public async Task SendUnitDeployRequest(Dictionary<string, string> deplotUnits, Action<UnitDeployPacketResultData> callback)
        => await SendWebRequest(UnitDeployPacketData.ROUTE, new MakeRequestUnitDeployPacket(deplotUnits).UnitDeployPacketData, callback);

    public async Task SendUserMonetaChangeRequest(EMonetaType monetaType, int amount, Action<UserMonetaChangePacketResultData> callback)
        => await SendWebRequest(UserMonetaChangePacketData.ROUTE, new MakeRequestUserMonetaChangePacket(monetaType, amount).UserMonetaChangePacketData, callback);

    public async Task SendPassiveUpgradeRequest(EPassiveType passiveType, Action<PassiveUpgrdePacketResultData> callback)
        => await SendWebRequest(PassiveUpgradePacketData.ROUTE, new MakeRequestPassiveUpgradePacket(passiveType).PassiveUpgradePacketData, callback);

    public async Task SendUpdateRankDataRequest(int point, Action<UpdatePvpRankPacketResultData> callback)
        => await SendWebRequest(UpdatePvpRankPacketData.ROUTE, new MakeUpdatePvpRankPacket(point).UpdatePvpRankPacketData, callback);

    public async Task SendGetRankDataRequest(Action<GetPvpRankPacketResultData> callback)
        => await SendWebRequest(GetPvpRankPacketData.ROUTE, new GetPvpRankPacketData(), callback);
}
