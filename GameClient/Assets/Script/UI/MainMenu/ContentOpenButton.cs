using System;
using UnityEngine;

public class ContentOpenButton : UIBase
{
    [SerializeField] private ContentUI contentUI;

    public event Action<ContentUI> OnContentOpened;

    protected override void Awake()
    {
        SignalRClient.I.ConnectToHub<MatchResultData>("MatchFail", OnReceiveFail);
        SignalRClient.I.ConnectToHub<MatchResultData>("MatchWaiting", OnReceiveWaiting);
        SignalRClient.I.ConnectToHub<MatchResultData>("MatchSucess", OnReceiveSucess);
    }

    public void Initialize()
    {
        contentUI.Initialize();
        contentUI.gameObject.SetActive(false);
    }

    public async void OnTouchButton()
    {
        OnContentOpened?.Invoke(contentUI);
        await SignalRClient.I.SendMessageToServer("JoinMatch", GameManager.I.MainUser.UserId);
    }
    private void OnReceiveSucess(MatchResultData data)
    {
        Debug.Log($"¸ÅÄª ¼º°ø: {data.UserId}, {data.OpponentUserId}");
    }

    private void OnReceiveWaiting(MatchResultData data)
    {
        Debug.Log($"¸ÅÄª ´ë±â: {data.UserId}, {data.UserConnectionId}");
    }

    private void OnReceiveFail(MatchResultData data)
    {
        Debug.Log($"¸ÅÄª ½ÇÆÐ: {data.UserId}");
    }
}