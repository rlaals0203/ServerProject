using UnityEngine;
using UnityEngine.UI;

public class UnitDeploySlotUI : UIBase
{
    [SerializeField] private Image unitIcon;

    public void Initialize(UnitSO unitData)
    {
        unitIcon.sprite = unitData.UnitIcon;
    }
}