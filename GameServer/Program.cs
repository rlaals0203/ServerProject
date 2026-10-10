using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
MongoDbSettings mongoDbSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDbSettings>() 
    ?? throw new InvalidOperationException("mongodb connection not found");

builder.Services.AddSignalR();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));
builder.Services.AddSingleton<DbManager>();
builder.Services.AddSingleton<GameDataManager>();
builder.Services.AddSingleton<GameRoomService>();
builder.Services.AddSingleton<UnitDataDefine>();
builder.Services.AddSingleton<StageDataDefine>();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

app.MapControllers();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRouting();
app.MapHub<GameHub>("/gamehub");
app.Run();
