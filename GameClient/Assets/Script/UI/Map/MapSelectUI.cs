using System;
using UnityEngine;

public class MapSelectUI : UIBase
{
    [SerializeField] private MapListUI mapListUI;

    protected override void Awake()
    {
        mapListUI.OnElementChanged += HandleMapChanged;
    }

    private void HandleMapChanged(MapElementUI ui)
    {
        var mapData = ui.GetMapData();
    }

    private void OnDestroy()
    {
        mapListUI.OnElementChanged -= HandleMapChanged;
    }
}