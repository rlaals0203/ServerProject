using Core;
using MINLib.Script.Core.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Stack<PopupUIBase> _uiStack = new();
    
    public T OpenPopup<T>(string key, Vector3 position, Transform parent, 
        Action spawnCallback = null, Action closeCallback = null) where T : PopupUIBase
    {
        T popup = ResourceManager.I.SpawnAndInitPool<T>(key, position, parent, spawnCallback);

        _uiStack.Push(popup);
        popup.CloseCallback += closeCallback;
        popup.OpenPopup();
        return popup;
    }

    public void CloseTopPopup()
    {
        var popup = _uiStack.Peek();
        popup.OnTouchClose();
    }
}
