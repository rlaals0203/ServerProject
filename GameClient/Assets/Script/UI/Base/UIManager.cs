using Core;
using MINLib.Script.Core.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Transform popupTrm;
    private Stack<PopupUIBase> _uiStack = new();
    
    public T OpenPopup<T>(string key, Vector3 position, Transform parent, 
        Action spawnCallback = null, Action closeCallback = null) where T : PopupUIBase
    {
        parent ??= popupTrm;
        T popup = ResourceManager.I.SpawnAndInitPool<T>(key, position, parent, spawnCallback);

        if(popup == null)
        {
            Debug.LogError($"{key} popup is null");
            return null;
        }

        _uiStack.Push(popup);
        popup.CloseCallback += closeCallback;
        popup.transform.position = position;
        popup.OpenPopup();
        return popup;
    }

    public void CloseTopPopup()
    {
        var popup = _uiStack.Peek();
        popup.OnTouchClose();
    }

    public void CloseAllPopup()
    {
        foreach(var popup in _uiStack)
        {
            if (popup == null)
                continue;

            popup.OnTouchClose();
        }
    }
}
