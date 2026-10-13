using TMPro;
using UnityEngine;

public class PvpUI : ContentUI
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text StatusText;

    protected override void Awake()
    {
        base.Awake();
        SignalRClient.I.ConnectToHub<JoinMatchResultData>(JoinMatchResultData.CONNECTION, OnRecieveResult);
    }
    public override async void Initialize()
    {
        await SignalRClient.I.SendMessageToServer("RequestJoinMatch", GlobalInstance.I.MainUser.UserId);
    }

    private void OnRecieveResult(JoinMatchResultData data)
    {
        Title.text = data.OpponentUser == null ? "상대를 찾는 중..." : $"{data.OpponentUser.UserId}와 매칭 성공!";
        StatusText.text = data.MatchResult.ToString();
    }
}