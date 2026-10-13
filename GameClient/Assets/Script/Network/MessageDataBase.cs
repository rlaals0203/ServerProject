using System;
public class MessageDataBase
{
    public string SessionToken { get; set; }
}

public class MessageResultDataBase
{
    public EPacketResult Result { get; set; }
    public DateTime Date { get; set; }

    public MessageResultDataBase(EPacketResult result)
    {
        Result = result;
        Date = DateTime.UtcNow.AddHours(9);
    }
}

public partial class StandardMessageData : MessageDataBase
{
    public string LoginUserId { get; set; }
    public StandardMessageData() { }
}

public partial class StandardMessageResultData : MessageResultDataBase
{
    public StandardMessageResultData(EPacketResult packetResult) : base(packetResult) { }
}