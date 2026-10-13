using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvpUI : ContentUI
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text StatusText;

    protected override void Awake()
    {
        base.Awake();
        SignalRClient.I.ConnectToHub<JoinMatchResultData>(JoinMatchResultData.CONNECTION, OnRecieveResult);
    }
    public override void Initialize()
    {
        SendMessage();
    }

    private async void SendMessage()
    {
        await SignalRClient.I.SendMessageToServer("RequestJoinMatch", GlobalInstance.I.MainUser.UserId);
    }

    private void OnRecieveResult(JoinMatchResultData data)
    {
        if(data.MatchResult == EMatchResult.Matched)
        {
            GlobalInstance.I.IsPvpOpponent = data.IsOpponent;
            StartPVP();
        }

        Title.text = data.OpponentUser == null ? "상대를 찾는 중..." : $"{data.OpponentUser.UserId}와 매칭 성공!";
        StatusText.text = data.MatchResult.ToString();
    }

    private async UniTask StartPVP()
    {
        await UniTask.Delay(3000);
        await SceneManager.LoadSceneAsync(SceneDefine.PVP_SCENE);
    }
}