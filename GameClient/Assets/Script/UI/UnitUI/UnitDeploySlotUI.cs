using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeploySlotUI : UIBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image unitIcon;
    [SerializeField] private UIBlinkEffect blinkEffect;

    private int _index;

    public event Action<int, bool> OnHoverSlot;

    public void Initialize(UnitSO unitData)
    {
        unitIcon.sprite = unitData.UnitIcon;
        _index = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(_index, true);
        blinkEffect.PlayBlinkEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(_index, false);
        blinkEffect.StopBlinkEffect();
    }
}