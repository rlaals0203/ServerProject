using Core;
using System;
using System.Collections;
using System.Net;
using UnityEngine.Networking;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public IEnumerator SendWebRequest<TRequest, TResult>(string url, string route, Action<TResult> callback)
        where TRequest : PacketBase
        where TResult : PacketResultBase
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, route);
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
        }
    }
}
