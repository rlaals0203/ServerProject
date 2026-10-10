using System;
using UnityEngine;

public class MapSelectUI : UIBase
{
    [SerializeField] private MapDataSOList mapDataList;
    [SerializeField] private MapListUI mapListUI;
    [SerializeField] private MapMovement mapMovement; 

    protected override void Awake()
    {
        mapListUI.InitializeElement(mapDataList, HandleTouchPlay);
        mapListUI.OnElementChanged += HandleMapChanged;
    }

    private void HandleTouchPlay()
    {

    }

    private void HandleMapChanged(MapElementUI ui)
    {
        var mapData = ui.GetMapData();
        mapMovement.ChangeStage(Convert.ToInt32(mapData.MapId) - 1);
    }

    private void OnDestroy()
    {
        mapListUI.OnElementChanged -= HandleMapChanged;
    }
}