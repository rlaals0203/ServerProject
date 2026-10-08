using DG.Tweening;
using UnityEngine;

public delegate void OnUIStatusChanged(UIBase changed, bool status);

public class UIBase : MonoBehaviour
{
   [HideInInspector] public bool IsActive;
   [SerializeField] protected GameObject _outlineYellow;
   [SerializeField] protected GameObject _outlinePink;

    public RectTransform Rect => transform as RectTransform;
    public event OnUIStatusChanged OnStatusChanged;

    private Tween _outlineAnimTween;

    protected virtual void Awake()
    {

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _outlineYellow = transform.Find("Outline_Yellow")? gameObject : null;
        _outlinePink = transform.Find("Outline_Pink")? gameObject : null;
    }
#endif

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
