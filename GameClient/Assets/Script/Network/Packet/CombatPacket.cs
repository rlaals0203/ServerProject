using System;

[Serializable]
public class RequestDamagePacketData : PacketBase
{
    public string ROUTE = "RequestDamage";
    public UnitData[] UnitList { get; set; }
    public UnitData Attacker { get; set; }
    public double Damage { get; set; }
}

[Serializable]
public class RequestDamagePacketDataResult : PacketResultBase
{
    public UnitData[] UnitList { get; set; }
    public UnitData Attacker { get; set; }
    public double Damage { get; set; }

    public RequestDamagePacketDataResult(EPacketResult result) : base(result) { }
}