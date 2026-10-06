using UnityEngine;
using UnityEngine.UI;

public class UnitSpecialAbilityUI : UIBase
{
    [SerializeField] private Image[] targetTypes;
    [SerializeField] private GameObject[] specialAbilities;
    [SerializeField] private RectTransform rect;

    public void InitSpecialAbilityUI(UnitSpecialAbilityData specialAbilityData)
    {
        DisableImages();

        foreach (SpecialAbilityData abilityData in specialAbilityData.SpecialAbilityDatas)
        {
            specialAbilities[(int)abilityData.AbilityType].SetActive(true);
        }

        foreach (EUnitType targetType in specialAbilityData.TargetTypes)
        {
            targetTypes[(int)targetType].color = Color.white;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }

    private void DisableImages()
    {
        foreach(var targetType in targetTypes)
        {
            targetType.color = Color.gray;
        }

        foreach(var specialAbility in specialAbilities)
        {
            specialAbility.gameObject.SetActive(false);
        }
    }
}