using System;
using UnityEngine;

public enum EContentType
{
    PVE,
    Upgrade,
    Deploy,
    PVP
}

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