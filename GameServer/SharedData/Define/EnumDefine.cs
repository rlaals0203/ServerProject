using System;

public enum EPacketResult
{
    Ok,
    LoginFailed,
    InvalidPassword,
    InvalidRequest,
    NotFoundUser,
    NotFoundUnit,
    MaxUnitLevel,
    Fail
}

public enum EMatchResult
{
    Matched,
    Waiting,
    Fail,
}

public enum EDbType
{
    BaseUserData,
    UserUnitData,
    UserMonetaData
}

public enum EDbResult
{
    Sucess,
    Fail
}

public enum EMonetaType
{
    Exp,
    CatFood
}

public enum EPassiveType
{
    CannonAttackPower,
    CannonRange,
    CannonCharge,
    WorkerCatRate,
    WorkerCatWallet,
    BaseDefense,
    Research,
    Accountant,
    Study,
    Energy
}

public enum EStatType
{
    AttackPower,
    Health,
    MoveSpeed,
    AttackRange,
    AttackDistance,
    DetectRange,
}

public enum EUnitClassType : ushort
{
    First = 1,
    Second = 2,
    Thired = 3
}

[Flags]
public enum EUnitType : short
{
    Normal  = 1 << 1,
    Fly     = 1 << 2,
    Red     = 1 << 3,
    Black   = 1 << 4,
    All     = 1 << 5,
}

public enum EUnitGradeType
{
    Normal,
    Rare,
    SuperRare,
    UltraSuperRare
}

[Flags]
public enum ESpecialAbilityType : uint
{
    SingleAttack = 1 << 1,                  //개체 공격
    AreaAttack = 1 << 2,                    //범위 공격
    LongDistance = 1 << 3,                  //원거리 공격
    Weaken = 1 << 4,                        //공격력 다운
    Freeze = 1 << 5,                        //멈춤
    Slow = 1 << 6,                          //느리게
    StrongAgainst = 1 << 7,                 //엄청 강하다
    Resistant = 1 << 8,                     //맷집이 좋다
    MassiveDamage = 1 << 9,                 //초데미지
    Knockback = 1 << 10,                    //넉백
    AntiWeaken = 1 << 11,                   //공격력 다운 무효
    AntiFreeze = 1 << 12,                   //멈춤 무효
    AntiSlow = 1 << 13,                     //느리게 무효
    AntiKnockback = 1 << 14,                //넉백 무효
}

public enum ELayer
{
    Unit,
    Enemy
}

public enum EStageSpawnTrigger
{
    BattleStart,
    EnemyCastleHit
}
