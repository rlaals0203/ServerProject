using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeploySlotUI : UIBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image unitIcon;
    [SerializeField] private UIBlinkEffect blinkEffect;

    public event Action<UnitDeploySlotUI, bool> OnHoverSlot;

    public void Initialize(UnitSO unitData)
    {
        unitIcon.sprite = unitData.UnitIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(this, true);
        blinkEffect.PlayBlinkEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(this, false);
        blinkEffect.StopBlinkEffect();
    }
}