using MINLib.Script.Core.Resource;
using System;
using UnityEngine;

public struct OpenPopup<T> where T : PopupUIBase
{
    public T spawnedPopup;

    public OpenPopup(string key, Vector3 position, Transform parent = null, Action spawnCallback = null, Action closeCallback = null)
    {
        T popup = UIManager.I.OpenPopup<T>(key, position, parent, spawnCallback, closeCallback);
        spawnedPopup = popup;
    }
}

public struct SpawnUI<T> where T :UIBase
{
    public T spawned;

    public SpawnUI(string key, Vector3 position, Transform parent = null, Action spawnCallback = null)
    {
        spawned = new SpawnCacheObject<T>(key, position, parent, spawnCallback).Spawned;
    }
}

public struct SpawnCacheObject<T> 
{
    public T Spawned;

    public SpawnCacheObject(string key, Vector3 position, Transform parent = null, Action spawnCallback = null)
    {
        Spawned = ResourceManager.I.SpawnAndInitPool<T>(key, position, parent, spawnCallback);
    }
}
