using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDeploySlotUI : UIBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image unitIcon;
    [SerializeField] private UIBlinkEffect blinkEffect;

    public event Action<int, bool> OnHoverSlot;

    public void Initialize(UnitSO unitData)
    {
        if(unitData == null)
        {
            unitIcon.gameObject.SetActive(false);
            return;
        }


        unitIcon.sprite = unitData.UnitIcon;
        unitIcon.gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(GetIndex(), true);
        blinkEffect.PlayBlinkEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverSlot?.Invoke(GetIndex(), false);
        blinkEffect.StopBlinkEffect();
    }

    public int GetIndex() => transform.GetSiblingIndex();
}