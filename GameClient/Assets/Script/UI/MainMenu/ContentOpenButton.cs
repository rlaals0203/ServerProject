using System;
using UnityEngine;

public class ContentOpenButton : UIBase
{
    [SerializeField] private ContentUI contentUI;

    public event Action<ContentUI> OnContentOpened;

    public void OnTouchButton()
    {
        OnContentOpened?.Invoke(contentUI);
    }
}