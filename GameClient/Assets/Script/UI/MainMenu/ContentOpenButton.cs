using System;
using UnityEngine;

public class ContentOpenButton : UIBase
{
    [SerializeField] private ContentUI contentUI;

    public event Action<ContentUI> OnContentOpened;

    protected override void Awake()
    {
        contentUI.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        contentUI.Initialize();
    }

    public void OnTouchButton()
    {
        OnContentOpened?.Invoke(contentUI);
    }
}