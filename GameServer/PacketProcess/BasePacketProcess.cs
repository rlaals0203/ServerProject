using MongoDB.Driver;

public class BasePacketProcess
{
    protected IMongoClient client;
    protected ILoggerFactory loggerFactory;
    public EPacketResult Result;

    public BasePacketProcess()
    {
        Result = EPacketResult.Ok;
        client = new MongoClient();
        loggerFactory = new LoggerFactory();
    }
}