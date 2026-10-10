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
            int index = (int)Mathf.Log((uint)abilityData.AbilityType, 2) - 1;

            if (index >= 0 && index < specialAbilities.Length)
            {
                specialAbilities[index].SetActive(true);
            }
        }

        foreach (EUnitType targetType in specialAbilityData.TargetTypes)
        {
            int index = (int)Mathf.Log((int)targetType, 2) - 1;

            if (index >= 0 && index < targetTypes.Length)
            {
                targetTypes[index].color = Color.white;
            }
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