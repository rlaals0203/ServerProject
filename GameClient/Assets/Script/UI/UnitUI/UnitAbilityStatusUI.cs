using UnityEngine;
using UnityEngine.UI;

public class UnitAbilityStatusUI : MonoBehaviour
{
    [SerializeField] private GameObject weakenUI;
    [SerializeField] private GameObject freezeUI;
    [SerializeField] private GameObject slowUI;
    [SerializeField] private FreezeUI freezeAnim;

    public void SetAbilityStatusUI(ESpecialAbilityType abilityType, bool status)
    {
        if ((abilityType & ESpecialAbilityType.Weaken) != 0)
        {
            weakenUI.SetActive(status);
        }
        if ((abilityType & ESpecialAbilityType.Freeze) != 0)
        {
            freezeUI.SetActive(status);

            if(status)
            {
                freezeAnim.PlayAnim();
            }
        }
        if ((abilityType & ESpecialAbilityType.Slow) != 0)
        {
            slowUI.SetActive(status);
        }
    }

    public void UpdateAbilityStatusUI(ESpecialAbilityType abilityType)
    {
        if ((abilityType & ESpecialAbilityType.Freeze) != 0)
        {
            freezeAnim.PlayAnim();
        }
    }
}