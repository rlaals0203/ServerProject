using DG.Tweening;
using UnityEngine;

public delegate void OnUIStatusChanged(UIBase changed, bool status);

public class UIBase : MonoBehaviour
{
    [HideInInspector] public bool IsActive;

    public RectTransform Rect => transform as RectTransform;
    public event OnUIStatusChanged OnStatusChanged;

    private Tween _outlineAnimTween;

    protected virtual void Awake()
    {
    }

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

    public void ActivateOutlineAnim()
    {
        _outlineAnimTween?.Kill();
    }
}
