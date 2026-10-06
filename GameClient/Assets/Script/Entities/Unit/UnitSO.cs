using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "SO/Unit")]
public class UnitSO : ScriptableObject
{
    public string UnitId;
    public string UnitName;
    public string UnitDesc;
    public Sprite UnitIcon;
    public int UpgradePrice;

    [Header("Unit Stats")]
    public int UnitCost;
    public EUnitType UnitType;
    public int UnitHealth;
    public int UnitAttackPower;
    public float UnitAttackTime;
    public float UnitAttackRange;
    public float UnitMoveSpeed;
    public int UnitHitbackCount;
    public int UnitDropMoneyAmount;
    
    public UnitSpecialAbilityData UnitSpecialAbilityData;
}
