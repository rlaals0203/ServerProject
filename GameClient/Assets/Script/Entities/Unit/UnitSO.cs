using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "SO/Unit")]
public class UnitSO : ScriptableObject
{
    public string UnitName;
    public string UnitDesc;
    public Sprite UnitIcon;

    [Header("Unit Stats")]
    public EUnitType UnitType;
    public double UnitHealth;
    public double UnitAttackPower;
    public double UnitAttackTime;
    public double UnitAttackRange;
    public double UnitMoveSpeed;
    public int UnitHitbackCount;
    public int UnitDropMoneyAmount;
}
