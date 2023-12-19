using Worden_SocialMediaSite.Services;
using Worden_SocialMediaSite.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ServiceInterface, PostService>();

builder.Services.AddDbContext<SocialMediaDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("myDB"))
    );

builder.Services.AddIdentity<Account, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 5;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.User.RequireUniqueEmail = true;

    }
    ).AddEntityFrameworkStores<SocialMediaDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Accounts/Login";
    options.AccessDeniedPath = "/Accounts/Login";
});

var app = builder.Build();
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<SocialMediaDbContext>();
//context.Database.EnsureDeleted(); //if our database exists, then erase it! - only want this while developing the code
context.Database.EnsureCreated(); //if our database does not exist, then create it!


app.UseDefaultFiles();
app.UseStaticFiles();

//middleware pipeline
if (app.Environment.IsDevelopment())
{
    //get detailed error page ...
    app.UseDeveloperExceptionPage();
}
else
{
    //get friendly error page
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/Error");
}


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


 app.MapControllerRoute(
    name: "foryou",
    pattern: "foryou",
    defaults: new { controller = "Home", action = "ForYouPage" });

app.MapControllerRoute(
    name: "following",
    pattern: "following",
    defaults: new { controller = "Post", action = "Index" });

app.MapDefaultControllerRoute();






app.Run();
