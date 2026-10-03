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
    private EContentType _contentType;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    public virtual void Initialize(EContentType contentType)
    {
        _contentType = contentType;
    }
}