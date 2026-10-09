using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "SO/Unit")]
public class UnitSO : ScriptableObject
{
    [Header("Unit Info")]
    public string UnitId;
    public string UnitName;
    public string UnitDesc;
    public Sprite UnitIcon;
    public int UnlockPrice;
    public int UpgradePrice;

    [Header("Unit Stats")]
    public int UnitCost;
    public EUnitType[] UnitType;
    public float UnitCooldown;
    public float UnitMoveSpeed;
    public int UnitHitbackCount;
    public int UnitDropMoneyAmount;

    [Header("Combat Stats")]
    public int UnitAttackPower;
    public int UnitHealth;
    public float UnitAttackRange;
    public float UnitAttackDistance;
    public float DetectRange;
    public float AttackSpeed;
    
    public UnitSpecialAbilityData UnitSpecialAbilityData;
    public bool isDefaultUnit;

    [Header("AnimationSetting")]
    public AnimatorOverrideController animator;
}
