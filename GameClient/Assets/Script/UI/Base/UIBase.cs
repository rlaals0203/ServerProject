using System;
using UnityEngine;

public delegate void OnUIStatusChanged(UIBase changed, bool status);

public class UIBase : MonoBehaviour
{
    public bool IsActive;
    public RectTransform Rect => transform as RectTransform;
    public event OnUIStatusChanged OnStatusChanged;

    public void ShowUI()
    {
        ChangeUIStatus(true);
    }

    public void HideUI()
    {
        ChangeUIStatus(false);
    }

    private void ChangeUIStatus(bool isActive)
    {
        gameObject.SetActive(isActive);
        IsActive = isActive;
        OnStatusChanged?.Invoke(this, isActive);
    }
}
