using System;
using TMPro;
using UnityEngine;

public class MapElementUI : UIBase
{
    [SerializeField] private TMP_Text stageTitle;

    private MapDataSO _mapData;
    private Action<MapDataSO> _onTouchPlay;

    public void Initialize(MapDataSO mapData, string mapName, Action<MapDataSO> onTouchPlay)
    {
        _mapData = mapData;
        _onTouchPlay = onTouchPlay;
        stageTitle.text = mapName;
    }

    public void OnTouchPlay()
    {
        _onTouchPlay?.Invoke(_mapData);
    }

    public MapDataSO GetMapData() => _mapData;
}
