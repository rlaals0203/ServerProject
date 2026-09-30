using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPacketResult
{
    Ok,
    Fail
}

public enum EStatType
{
    AttackPower,
    Health,
    MoveSpeed,
}
public enum EUnitType
{
    Normal,
    Fly,
    Red,
    Black,
    All,
    None
}

public enum ESpecialAbilityType
{
    SingleAttack,               //개체 공격
    AreaAttack,                 //범위 공격
    LongDistance,               //원거리 공격
    [ProbabilityAbility]
    Weaken,                     //공격력 다운
    [ProbabilityAbility]
    Freeze,                     //멈춤
    [ProbabilityAbility]
    Slow,                       //느리게
    StrongAgainst,              //엄청 강하다
    Resistant,                  //맷집이 좋다
    MassiveDamage,              //초데미지
    [ProbabilityAbility]
    Knockback,                  //넉백
    AntiWeaken,                 //공격력 다운 무효
    AntiFreeze,                 //멈춤 무효
    AntiSlow,                   //느리게 무효
    AntiKnockback,              //넉백 무효
    None
}