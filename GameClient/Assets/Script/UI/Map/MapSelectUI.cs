using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectUI : ContentUI
{
    [SerializeField] private MapDataSOList mapDataList;
    [SerializeField] private MapListUI mapListUI;
    [SerializeField] private MapMovement mapMovement; 

    protected override void Awake()
    {
        mapListUI.InitializeElement(mapDataList, HandleTouchPlay);
        mapListUI.OnElementChanged += HandleMapChanged;
    }

    private void HandleTouchPlay(MapDataSO mapData)
    {
        GlobalInstance.I.CurrentMap = mapData;
        SceneManager.LoadScene("BattleScene");
    }

    public void OnTouchPlay()
    {
        GlobalInstance.I.CurrentMap = mapListUI.GetCurrentMap().GetMapData();
        SceneManager.LoadScene("BattleScene");
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
