using System;
using UnityEngine;

public class ContentUI : UIBase
{

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public virtual void Initialize()
    {
    }
}