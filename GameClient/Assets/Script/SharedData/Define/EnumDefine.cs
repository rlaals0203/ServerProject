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

public enum EUnitClassType
{
    First = 1,
    Second = 2,
    Thired = 3
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
    Knockback,                  //넉백
    StrongAgainst,              //엄청 강하다
    Resistant,                  //맷집이 좋다
    MassiveDamage,              //초데미지
    [ProbabilityAbility]
    AntiWeaken,                 //공격력 다운 무효
    AntiFreeze,                 //멈춤 무효
    AntiSlow,                   //느리게 무효
    AntiKnockback,              //넉백 무효
    None
}