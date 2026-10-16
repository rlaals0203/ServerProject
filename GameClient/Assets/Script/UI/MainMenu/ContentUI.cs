using System;
using UnityEngine;

public class ContentUI : UIBase
{

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Initialize() { }

    public virtual void OnTouchGoBack() { }
}