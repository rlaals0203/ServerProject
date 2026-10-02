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
