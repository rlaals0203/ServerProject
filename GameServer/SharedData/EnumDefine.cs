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
    All
}

public enum ESpecialAbilityType
{
    SingleAttack,               //개체 공격
    AreaAttack,                 //범위 공격
    LongDistance,               //원거리 공격
    Weaken,                     //공격력 다운
    Freeze,                     //멈춤
    Slow,                       //느리게
    StrongAgainst,              //엄청 강하다
    Resistant,                  //맷집이 좋다
    MassiveDamage,              //초데미지
    Knockback,                  //넉백
    AntiWeaken,                 //공격력 다운 무효
    AntiFreeze,                 //멈춤 무효
    AntiSlow,                   //느리게 무효
    AntiStrongAgainst,          //엄청 강하다 무효
    AntiResistant,              //맷집이 좋다 무효
    AntiMassiveDamage,          //초데미지 무효
    AntiKnockback,              //넉백 무효
}