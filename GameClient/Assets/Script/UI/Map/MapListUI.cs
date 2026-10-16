using System;
using UnityEngine;

public class MapListUI : SnapScrollViewBase<MapElementUI>
{
    public void InitializeElement(MapDataSOList mapDataList, Action<MapDataSO> onTouchPlay)
    {
        int stageNumber = 1;

        foreach(var mapData in mapDataList.MapDataList)
        {
            var spawned = new SpawnUI<MapElementUI>("MapElement", Vector3.zero, content).spawned;
            spawned.Initialize(mapData, mapDataList.GetMapDataById(mapData.MapId).MapName, onTouchPlay);
            _elements.Add(spawned);
            stageNumber++;

            if(GlobalInstance.I.MainUser.UserStageData < stageNumber)
            {
                return;
            }
        }
    }

    public MapElementUI GetCurrentMap() => _currentElement;

    protected override void ElementChanged() { }
}
