using System;
public class MessageDataBase
{
    public DateTime Date { get; set; }
    public MessageDataBase()
    {
        Date = DateTime.UtcNow.AddHours(9);
    }
}

public partial class StandardMessageData : MessageDataBase
{
    public string Route { get; set; }
    public string RoomId { get; set; }
    public StandardMessageData() { }
}