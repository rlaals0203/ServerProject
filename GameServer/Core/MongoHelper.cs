using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

public class MongoHelper<T>(IMongoClient mongoClient, string databaseName, ILoggerFactory loggerFactory) where T : class
{
    private readonly IMongoClient MongoClient = mongoClient;
    private readonly ILogger<MongoHelper<T>> Logger = loggerFactory.CreateLogger<MongoHelper<T>>();
    private readonly string DatabaseName = databaseName;
    private IMongoCollection<T> TypedCollection;
    private IMongoCollection<BsonDocument> BsonCollection;

    public void InitCollection(string collectionName)
    {
        var database = MongoClient.GetDatabase(DatabaseName);
        TypedCollection = database.GetCollection<T>(collectionName);
        BsonCollection = database.GetCollection<BsonDocument>(TypedCollection.CollectionNamespace.CollectionName);
    }

    private BsonDocument SerializeObject(T data)
    {
        BsonDocument doc = data.ToBsonDocument();

        if (doc.Contains("_id"))
        {
            doc.Remove("_id");
        }

        return doc;
    }

    private T DeserializeBsonDocument(BsonDocument bsonDocument)
    {
        return BsonSerializer.Deserialize<T>(bsonDocument);
    }

    private static FilterDefinition<TDoc> GetFilter<TDoc>(string id)
    {
        return Builders<TDoc>.Filter.Eq("_id", id);
    }

    public async Task<T> LoadAsync(string id)
    {
        try
        {
            FilterDefinition<BsonDocument> filter = GetFilter<BsonDocument>(id);
            BsonDocument bsonDocument = await BsonCollection.Find(filter).FirstOrDefaultAsync();
            return bsonDocument == null ? default : DeserializeBsonDocument(bsonDocument);
        }
        catch(Exception ex)
        {
            Logger.LogError($"MongoDbHelper LoadAsync Error. Exception : {ex}");
            return null;
        }
    }

    public async Task<EDbResult> SaveAsync(T data, string id)
    {
        try
        {
            FilterDefinition<T> filter = GetFilter<T>(id);
            BsonDocument doc = SerializeObject(data);
            BsonDocumentUpdateDefinition<T> update = new(new BsonDocument("$set", doc));
            UpdateOptions options = new() { IsUpsert = true };

            await TypedCollection.UpdateOneAsync(filter, update, options);
            return EDbResult.Sucess;
        }
        catch  
        {
            Logger.LogError($"MongoDbHelper Save Error");
            return EDbResult.Fail;
        }
    }
}