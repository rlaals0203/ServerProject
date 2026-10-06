using UnityEngine;
using UnityEngine.UI;

public class UnitSpecialAbilityUI : UIBase
{
    [SerializeField] private GameObject[] targetTypes;
    [SerializeField] private GameObject[] specialAbilities;

    public void InitSpecialAbilityUI(UnitSpecialAbilityData specialAbilityData)
    {
        DisableImages();

        foreach (SpecialAbilityData abilityData in specialAbilityData.SpecialAbilityDatas)
        {
            specialAbilities[(int)abilityData.AbilityType].SetActive(true);
        }

        foreach (EUnitType targetType in specialAbilityData.TargetTypes)
        {
            specialAbilities[(int)targetType].SetActive(true);
        }
    }

    private void DisableImages()
    {
        foreach(var targetType in targetTypes)
        {
            targetType.gameObject.SetActive(false);
        }

        foreach(var specialAbility in specialAbilities)
        {
            specialAbility.gameObject.SetActive(false);
        }
    }
}