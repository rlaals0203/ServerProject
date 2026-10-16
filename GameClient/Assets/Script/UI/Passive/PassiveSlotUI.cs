using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSlotUI : UpgradeSlotUI
{
    [SerializeField] protected TMP_Text unitNameText;
    [SerializeField] protected TMP_Text unitLevelText;
    [SerializeField] protected TMP_Text unitUpgradeCost;
    [SerializeField] protected TMP_Text maxLevelText;
    [SerializeField] protected Image unitIcon;
    [SerializeField] protected GameObject unitUpgradeUI;

    [SerializeField] private Sprite[] passiveIcons;

    private EPassiveType _passiveType;

    public void RefreshUI(EPassiveType passiveType, PassiveSO passiveSO)
    {
        _passiveType = passiveType;
        var passiveData = GlobalInstance.I.MainUser.UserPassiveData;
        string key = passiveType.ToString();
        int currentLevel = passiveData[key];

        unitLevelText.text = currentLevel.ToString();
        unitUpgradeCost.text = new GetUnitUpgradePrice(GameDefine.PassiveStartPrice, currentLevel).Price.ToString();
        unitNameText.text = passiveSO.PassiveName;
        maxLevelText.gameObject.SetActive(false);
        unitIcon.sprite = passiveIcons[(int)passiveType];

        if (currentLevel == GameDefine.PassiveMaxLevel)
        {
            unitUpgradeUI.SetActive(false);
            maxLevelText.gameObject.SetActive(true);
        }
    }

    public EPassiveType GetPassiveType() => _passiveType;
}