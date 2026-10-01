using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

[Serializable]
public class UserData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    public UserUnitData UserUnitData { get; set; }
}