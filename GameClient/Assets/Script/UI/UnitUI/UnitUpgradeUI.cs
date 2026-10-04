using UnityEngine;

public class UnitUpgradeUI : ContentUI
{
    [SerializeField] private UnitSOList unitSOList;
    [SerializeField] private UnitListUI unitListUI;

    public override void Initialize(EContentType contentType)
    {
        base.Initialize(contentType);
    }
}