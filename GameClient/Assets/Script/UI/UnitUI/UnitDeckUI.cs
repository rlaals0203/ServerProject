using System;
using System.Collections.Generic;

public class UnitDeckUI : UIBase
{
    private UnitDeploySlotUI[] _deploySlots;
    private Dictionary<int, UnitData> _deployUnits;

    protected override void Awake()
    {
        base.Awake();
        _deploySlots = GetComponentsInChildren<UnitDeploySlotUI>();
    }

    private void RefreshInternal()
    {

    }

    public async void SaveDeployUnits()
    {
        await NetworkManager.I.SendUnitDeployRequest(_deployUnits, HandleSaveDeployUnits);
    }

    private void HandleSaveDeployUnits(UnitDeployPacketResultData data)
    {
        GameManager.I.User.UserUnitData.DeployUnitList = data.DeployUnits;
    }
}