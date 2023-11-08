using Worden_SocialMediaSite.Services;
using Microsoft.EntityFrameworkCore;
using Worden_SocialMediaSite.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ServiceInterface, PostService>();

builder.Services.AddDbContext<SocialMediaDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("myDB"))
    );

var app = builder.Build();
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<SocialMediaDbContext>();
context.Database.EnsureDeleted(); //if our database exists, then erase it! - only want this while developing the code
context.Database.EnsureCreated(); //if our database does not exist, then create it!


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();


 app.MapControllerRoute(
    name: "foryou",
    pattern: "foryou",
    defaults: new { controller = "Home", action = "ForYouPage" });

app.MapControllerRoute(
    name: "following",
    pattern: "following",
    defaults: new { controller = "Post", action = "Index" });

app.MapDefaultControllerRoute();


app.Use(async (context, next) =>
{
    await next.Invoke();
    if (context.Response.StatusCode == 404)
    {
        await context.Response.WriteAsync("<div style='color: red;'>Error, this page does not exist in the directory, or is currently in Development</div>");
    }
    else if (!context.Response.HasStarted)
    {
        await context.Response.WriteAsync("Please come back soon!!\n");
    }
});

app.Run();
