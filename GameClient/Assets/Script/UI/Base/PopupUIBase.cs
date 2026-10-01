using System;
using UnityEngine;

public abstract class PopupUIBase : UIBase
{
    public event Action CloseCallback;

    protected abstract void Initialize();

    public virtual void OnTouchClose()
    {
        HideUI();
        CloseCallback?.Invoke();
        CloseCallback = null;
    }

    public virtual void OpenPopup()
    {
        ShowUI();
        Initialize();
    }
}
