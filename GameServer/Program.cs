var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
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