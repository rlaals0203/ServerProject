using System;
using TMPro;
using UnityEngine;

public class PVPBattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private PVPCastleHandler pvpCastleHandler;
    [SerializeField] private UnitBattleDeckUI unitBattleDeckUI;

    private PVPUnitRenderer _unitRenderer;

    private IDisposable _battleSubscription;
    private IDisposable _castleSubscription;
    private IDisposable _hitEffectSubscription;
    private bool _isOpponent;

    private void Start()
    {
        _battleSubscription = SignalRClient.I.SubscribeToHub<PVPBattleInfoDataMessage>(PVPBattleInfoDataMessage.ROUTE, HandleUpdateUnits);
        _castleSubscription = SignalRClient.I.SubscribeToHub<UpdateCastleDataMessage>(UpdateCastleDataMessage.ROUTE, HandleUpdateCastle);
        _hitEffectSubscription = SignalRClient.I.SubscribeToHub<PlayHitEffectDataMessage>(PlayHitEffectDataMessage.ROUTE, HandlePlayHitEffect);
        _unitRenderer = new PVPUnitRenderer(unitSOList);
        _isOpponent = GlobalInstance.I.IsPvpOpponent;
    }

    private void HandlePlayHitEffect(PlayHitEffectDataMessage message)
    {
        float x = _isOpponent ? -message.PosX : message.PosX;
        bool isOpponent = message.IsOpponent != _isOpponent;
        new PlayHitEffect(new Vector3(x, message.PosY), isOpponent);
    }

    private void HandleUpdateCastle(UpdateCastleDataMessage message)
    {
        pvpCastleHandler.UpdateCastleHealthText(message, _isOpponent);
    }

    private void HandleUpdateUnits(PVPBattleInfoDataMessage data)
    {
        _unitRenderer.UpdateRenderer(data.AllUnitDatas);
        UpdateMoneyText(data);
    }

    private void UpdateMoneyText(PVPBattleInfoDataMessage data)
    {
        float money = _isOpponent ? data.OpponentCurrentMoney : data.CurrentMoney;
        moneyText.text = $"{(int)money}¿ø";
        unitBattleDeckUI.UpdatePvpDeck(data.UnitCooldownData, money, _isOpponent);
    }

    private void OnDestroy()
    {
        _battleSubscription.Dispose();
        _castleSubscription.Dispose();
        _hitEffectSubscription.Dispose();
        _unitRenderer.ReleaseRenderer();
    }
}