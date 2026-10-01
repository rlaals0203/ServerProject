using Core;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public const string URL = "http://localhost:5200";
    private HttpClient _httpClient;

    protected override void Awake()
    {
        base.Awake();

        var cookieContainer = new CookieContainer();
        var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        _httpClient = new HttpClient(handler);

    }

    public async Task SendWebRequest<TRequest, TResult>(string route, TRequest request, Action<TResult> callback)
        where TRequest : PacketBase
        where TResult : PacketResultBase
    {
        string json = JsonUtility.ToJson(request);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync($"{URL}/{route}", content);

        string result = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            TResult resultPacket = JsonUtility.FromJson<TResult>(result);
            callback?.Invoke(resultPacket);
            return;
        }

        Debug.LogError($"POST {route} failed");
    }
}