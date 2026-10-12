using System;
using UnityEngine;

public class MapListUI : SnapScrollViewBase<MapElementUI>
{
    public void InitializeElement(MapDataSOList mapDataList, Action<MapDataSO> onTouchPlay)
    {
        foreach(var mapData in mapDataList.MapDataList)
        {
            var spawned = new SpawnUI<MapElementUI>("MapElement", Vector3.zero, content).spawned;
            spawned.Initialize(mapData, mapDataList.GetMapDataById(mapData.MapId).MapName, onTouchPlay);
            _elements.Add(spawned);
        }
    }

    public MapElementUI GetCurrentMap() => _currentElement;

    protected override void ElementChanged() { }
}
