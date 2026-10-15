using MongoDB.Bson.Serialization.Attributes;

[Serializable]
public class UserBaseData
{
    [BsonId]
    public string UserId { get; set; }
    public string Password { get; set; }
    public int UserStageData { get; set; }
    public Dictionary<string, int> UserMonetaData { get; set; }
    public Dictionary<string, int> UserPassiveData { get; set; }
    public UserUnitData UserUnitData { get; set; }
}
