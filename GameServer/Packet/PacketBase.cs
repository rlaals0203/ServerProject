public class PacketDataBase
{
    public string? SessionToken { get; set; }
}

public class PacketResultDataBase
{
    public EPacketResult Result { get; set; }
    public DateTime Date { get; set; }

    public PacketResultDataBase(EPacketResult result)
    {
        Result = result;
        Date = DateTime.UtcNow.AddHours(9);
    }
}

public partial class StandardPacketData : PacketDataBase
{
    public string? LoginUserId  { get; set; }
    public StandardPacketData() { }
}

public partial class StandardPacketResultData : PacketResultDataBase
{
    public StandardPacketResultData(EPacketResult packetResult) : base(packetResult) { }
}