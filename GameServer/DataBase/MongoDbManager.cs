using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbManager
{
    public async Task UpdateGoldByKeyAsync(string stringKey, int newGold)
    {
        var objectId = ObjectId.Parse(stringKey);
        
    }
}