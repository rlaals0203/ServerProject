using System;

[Serializable]
public class RequestPacketData : StandardPacketData
{
    public string ROUTE = "RequestDamage";
    public UnitData[] UnitList { get; set; }
    public UnitData Attacker { get; set; }
    public double Damage { get; set; }
}

[Serializable]
public class RequestDamagePacketDataResult : StandardPacketResultData
{
    public UnitData[] UnitList { get; set; }
    public UnitData Attacker { get; set; }
    public double Damage { get; set; }

    public RequestDamagePacketDataResult(EPacketResult result) : base(result) { }
}