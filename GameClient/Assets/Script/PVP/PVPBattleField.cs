using System;
using TMPro;
using UnityEngine;

public class PVPBattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private PVPCastleHandler pvpCastleHandler;

    private PVPUnitRenderer _unitRenderer;

    private PVPBattleInfoDataMessage _updateUnitData;
    private IDisposable _subscription;
    private bool _isOpponent;


    private void Start()
    {
        _subscription = SignalRClient.I.SubscribeToHub<PVPBattleInfoDataMessage>(PVPBattleInfoDataMessage.ROUTE, HandleUpdateUnits);
        _subscription = SignalRClient.I.SubscribeToHub<UpdateCastleDataMessage>(UpdateCastleDataMessage.ROUTE, HandleUpdateCastle);
        _unitRenderer = new PVPUnitRenderer(unitSOList);
        _isOpponent = GlobalInstance.I.IsPvpOpponent;
    }

    private void HandleUpdateCastle(UpdateCastleDataMessage message)
    {
        pvpCastleHandler.UpdateCastleHealthText(message, _isOpponent);
    }

    private void HandleUpdateUnits(PVPBattleInfoDataMessage data) => _updateUnitData = data;

    private void Update()
    {
        if (_updateUnitData != null)
        {
            _unitRenderer.UpdateRenderer(_updateUnitData.AllUnitDatas);
            UpdateMoneyText();
        }
    }

    private void UpdateMoneyText()
    {
        float money = _isOpponent ? _updateUnitData.OpponentCurrentMoney : _updateUnitData.CurrentMoney;
        moneyText.text = $"{(int)money}¿ø";
    }

    private void OnDestroy()
    {
        if(_subscription != null)
            _subscription.Dispose();

        _unitRenderer.ReleaseRenderer();
    }
}