using MINLib.Script.Core.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PVPBattleField : MonoBehaviour
{
    [SerializeField] private UnitSOList unitSOList;

    private PVPUnitRenderer _unitRenderer;

    private UpdateAllUnitDataMessage _updateUnitData;
    private IDisposable _subscription;


    private void Start()
    {
        _subscription = SignalRClient.I.SubscribeToHub<UpdateAllUnitDataMessage>(UpdateAllUnitDataMessage.ROUTE, HandleUpdateUnits);
        _unitRenderer = new PVPUnitRenderer(unitSOList);
    }

    private void HandleUpdateUnits(UpdateAllUnitDataMessage data) => _updateUnitData = data;

    private void Update()
    {
        _unitRenderer.UpdateRenderer(_updateUnitData);
    }

    private void OnDestroy()
    {
        if(_subscription != null)
            _subscription.Dispose();

        _unitRenderer.ReleaseRenderer();
    }
}