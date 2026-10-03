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

    public virtual void Initialize(EContentType contentType)
    {
        _contentType = contentType;
    }
}