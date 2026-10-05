using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoClient>(new MongoClient("mongodb+srv://scientistmin_db_user:<db_password>@defensecluster.5ibyqfa.mongodb.net/?appName=DefenseCluster"));
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<DbManager>();
builder.Services.AddSingleton<GameDataManager>();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

app.MapControllers();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRouting();
app.Run();