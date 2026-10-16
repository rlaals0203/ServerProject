using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvpUI : ContentUI
{
    [SerializeField] private TMP_Text Title;
    private IDisposable _subscription;

    protected override void Awake()
    {
        base.Awake();
        _subscription = SignalRClient.I.SubscribeToHub<JoinMatchResultData>(JoinMatchResultData.CONNECTION, OnRecieveResult);
    }
    public override async void Initialize()
    {
        await SignalRClient.I.SendMessageToServer("RequestJoinMatch", GlobalInstance.I.MainUser.UserId);
    }

    public override async void OnTouchGoBack()
    {
        await SignalRClient.I.SendMessageToServer("RequestLeaveMatch", GlobalInstance.I.MainUser.UserId);
    }

    private void OnRecieveResult(JoinMatchResultData data)
    {
        if(data.MatchResult == EMatchResult.Matched)
        {
            GlobalInstance.I.IsPvpOpponent = data.IsOpponent;
            StartPVP();
        }

        Title.text = data.OpponentUser == null ? "상대를 찾는 중..." : $"{data.OpponentUser.UserId}와 매칭 성공!";
    }

    private async UniTask StartPVP()
    {
        await UniTask.Delay(3000);
        await SceneManager.LoadSceneAsync(SceneDefine.PVP_SCENE);
    }

    private void OnDestroy()
    {
        _subscription.Dispose();
    }
}