using Core;
using System;
using System.Collections.Generic;

public class UIManager : MonoSingleton<UIManager>
{
    private Stack<PopupUIBase> _uiStack = new();
    
    public void OpenPopup<T>(T popup, Action closeCallback = null) where T : PopupUIBase
    {
        _uiStack.Push(popup);
        popup.CloseCallback += closeCallback;
        popup.OpenPopup();
    }

    public void CloseTopPopup()
    {
        var popup = _uiStack.Peek();
        popup.OnTouchClose();
    }
}
