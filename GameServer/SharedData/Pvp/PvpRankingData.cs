using MongoDB.Bson.Serialization.Attributes;

[Serializable]
public class PvpRankingData
{
    [BsonId]
    public string Id { get; set; }
    public Dictionary<string, UserPvpRankData> Ranks { get; set; }
    public DateTime UpdatedTime { get; set; }
}

[Serializable]
public class UserPvpRankData
{
    public string UserId { get; set; }
    public int Point { get; set; }
}