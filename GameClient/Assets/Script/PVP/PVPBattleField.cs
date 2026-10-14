using System;
using TMPro;
using UnityEngine;

public class PVPBattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private TMP_Text moneyText;

    private PVPUnitRenderer _unitRenderer;

    private PVPBattleInfoDataMessage _updateUnitData;
    private IDisposable _subscription;


    private void Start()
    {
        _subscription = SignalRClient.I.SubscribeToHub<PVPBattleInfoDataMessage>(PVPBattleInfoDataMessage.ROUTE, HandleUpdateUnits);
        _unitRenderer = new PVPUnitRenderer(unitSOList);
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
        float money = GlobalInstance.I.IsPvpOpponent ? _updateUnitData.OpponentCurrentMoney : _updateUnitData.CurrentMoney;
        moneyText.text = ((int)(money)).ToString();
    }

    private void OnDestroy()
    {
        if(_subscription != null)
            _subscription.Dispose();

        _unitRenderer.ReleaseRenderer();
    }
}